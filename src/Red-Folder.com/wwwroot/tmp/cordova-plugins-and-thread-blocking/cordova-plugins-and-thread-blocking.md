I've had two of my Cordova plugins suffer from poor thread handling (entirely on my part).

Both plugins reporting:
<blockquote class="tr_bq"><span style="background-color: white; color: #333333; line-height: 23.799999237060547px;"><span style="font-family: Courier New, Courier, monospace;">THREAD WARNING: exec() call to [pluginname].[method] blocked the main thread for XXms. Plugin should use CordovaInterface.getThreadPool().</span></span></blockquote>Having spent some time on the Scheduler plugin I've realized that the problems where based on simple misunderstanding by myself.

### Two issues

I have the blocking thread issue on two plugins:


* Background Service Plugin -> https://github.com/Red-Folder/Cordova-Plugin-BackgroundService/issues/19
* Scheduler Service Plugin -> https://github.com/Red-Folder/Scheduler-Plugin/issues/22

Generally most of the time the plugins worked - it was only on occasion that the THREAD WARNING would fire.  Within the Background Service Plugin it generally seemed to be because developers where not using the async calls correctly - most problems where resolved by simply chaining the async methods correctly.
The Scheduler plugin however was easier to replicate by simply adding multiple alarms - thus easier to track down.
The rest of this article discusses how I resolved the problem with the Scheduler Plugin.  I'll be applying the same sort of fixes to the Background Service plugin in due course.<h3>
</h3>### Problem 1 - Calls from the runnable thread to the plugin thread

Within my Scheduler Plugin, I already had the following within the execute method:
<pre>
</pre><blockquote class="tr_bq"><span style="font-family: Courier New, Courier, monospace;">cordova.getThreadPool().execute(new Runnable() {</span></blockquote><blockquote class="tr_bq"><span style="font-family: Courier New, Courier, monospace;">   @Override</span></blockquote><blockquote class="tr_bq"><span style="font-family: Courier New, Courier, monospace;">   public void run() {</span></blockquote><blockquote class="tr_bq"><span style="font-family: Courier New, Courier, monospace;">      ...</span><span style="font-family: Courier New, Courier, monospace;"></span></blockquote><blockquote class="tr_bq"><span style="font-family: Courier New, Courier, monospace;">   }</span><span style="font-family: Courier New, Courier, monospace;"> </span></blockquote><blockquote class="tr_bq"><span style="font-family: Courier New, Courier, monospace;">});</span></blockquote>

Within the run method, I would then call methods within the Plugin class.  And this was my first problem.
The Plugin code would run in thread 1.  On execute the Plugin code would generate a new runnable to handle the plugin request - this would be on thread 2.  Thread 1 (the Plugin) would return control to the UI.  The code on thread 2 would then run, at the appropriate point it would then call methods in the Plugin class (on thread 1).  While the code was running on thread 1, any further requests to the plugin from UI would block until it had finished.
To fix this, I moved all of the Scheduler methods into a new class called ScheduelerLogic.  This class implemented Runnable and included the run method.
The new SchedulerLogic was then initiated from the Plugin code (instead of the anonymous Runnable code from earlier):
<blockquote class="tr_bq"><span style="font-family: Courier New, Courier, monospace;">cordova.getThreadPool().execute(new SchedulerLogic(context, action, data, callback));</span></blockquote>
Because all of the "logic" was now within the Runnable class it was contained in the thread that the ThreadPool created - thus didn't block the original Plugin thread.<h3>
</h3>### Problem 2 - Blocking code on the constructor

Oddly I still had THREAD WARNINGS.
I had inadvertently added an additional blocking problem, by putting initialization code in the constructor of the SchedulerLogic plugin.
My original DAL (data access layer) was using a JSON string stored in Shared Preferences.  This worked ok, but wasn't very practical for thread concurrence (see below).  I loaded the DAL object within the SchedulerLogic constructor, this took time (and some clashing resources) which blocked the plugin thread - because the SchedulerLogic was created on thread 1.  It would be later run on its own thread, but the actual creation was on thread 1 - so any activity within the constructor would eat up milliseconds (and potentially run blocking code).
Moving the DAL creation out of the constructor (along with the solution to problem 1) solved the THREAD WARNING.<h3>
</h3>### Problem 3 - Concurrency thread saftey - or rather than lack of

So no more warnings.
But, when I tested the plugin creating multiple alarms (50 alarms), I found that not all of the alarms where actually created.  Debugging the code I found that my DAL wasn't thread safe and the data stored in the Shared Preferences wasn't reliable.
Basically there was no protection from multiple threads making requests to the DAL - this became a race condition between multiple threads - often resulting in two (or more) threads believing they had created an alarm record - but the DAL only having created one entry.
I could probably had added some threading logic to that DAL to handle the situation - however give the high volume of data I decided to migrate it to SQLite.
Guess what however, it still failed.<h3>
</h3>### Problem 4 - SQLite thread concurrency

Ok, so SQLite behind the scenes treats the database as a file.  So again, there needed to be protection against multiple thread opening the underlying file.
The error I was receiving was similar to:<span class="pln" style="font-family: monospace, serif; font-size: 0.875rem; line-height: 1.25rem; white-space: pre-wrap;">
</span><blockquote class="tr_bq"><span style="font-family: Courier New, Courier, monospace;"><span class="pln" style="line-height: 1.25rem; white-space: pre-wrap;">android</span><strong style="color: #333333; line-height: 1.25rem; white-space: pre-wrap;"><span class="pun" style="color: #666600;">.</span></strong><span class="pln" style="line-height: 1.25rem; white-space: pre-wrap;">database</span><strong style="color: #333333; line-height: 1.25rem; white-space: pre-wrap;"><span class="pun" style="color: #666600;">.</span></strong><span class="pln" style="line-height: 1.25rem; white-space: pre-wrap;">sqlite</span><strong style="color: #333333; line-height: 1.25rem; white-space: pre-wrap;"><span class="pun" style="color: #666600;">.</span></strong><span class="typ" style="color: #660066; line-height: 1.25rem; white-space: pre-wrap;">SQLiteDatabaseLockedException</span><strong style="color: #333333; line-height: 1.25rem; white-space: pre-wrap;"><span class="pun" style="color: #666600;">:</span></strong><span class="pln" style="line-height: 1.25rem; white-space: pre-wrap;"> database </span><span class="kwd" style="color: #000088; line-height: 1.25rem; white-space: pre-wrap;">is</span><span class="pln" style="line-height: 1.25rem; white-space: pre-wrap;"> locked </span><strong style="color: #333333; line-height: 1.25rem; white-space: pre-wrap;"><span class="pun" style="color: #666600;">(</span></strong><span class="pln" style="line-height: 1.25rem; white-space: pre-wrap;">code </span><span class="lit" style="color: #006666; line-height: 1.25rem; white-space: pre-wrap;">5</span><strong style="color: #333333; line-height: 1.25rem; white-space: pre-wrap;"><span class="pun" style="color: #666600;">)</span></strong></span></blockquote>
Luckily all the answers to my problems where found here in this blog: http://blog.lemberg.co.uk/concurrent-database-access
The blog provides a solution which controls multiple thread accessing the database.  I won't repeat what is an excellent explanation - go read it.<h3>
</h3>### And success

That's it.  All working.  I can now create a batch of 50 alarms (I'd expect more than anyone is likely to need).
Before I upload the code, I plan on putting some automated testing in place - linked into my Continuous Integration setup.  Hopefully should be covering that in my next blog.
