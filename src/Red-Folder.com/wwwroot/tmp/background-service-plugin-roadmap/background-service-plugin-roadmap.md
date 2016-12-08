Ok, so been a while since any posts.

There are a number of outstanding tasks for me to complete on the Background Service Plugin, so I thought I'd outline the roadmap for the coming couple of months.


## Automated build &amp; test
Current focus is on an automated build and test for the plugin.  Currently, due to the number of Cordova versions, it is rather time consuming to keep up with the Cordova version releases.  I think at the moment I'm about 2 versions behind.

I currently have a copy of Jenkins building the .jar against multiple versions of Cordova.  Next step is to produce a test project using Jasmine to BDD test the basic functions of each version of the Plugin.  This would get bundled up into a Jenkins job which I can run adhoc to perform a lot of time intensive tasks for me.

(or at least that the idea).

Hopefully I should have this done by the end of July.  I'll blog about the end result (along with some helper code) in early August all going well.

## Support for new Cordova versions
Following, the above then HOPEFULLY it should be easy to compile up the new Cordova versions (I'd expect to be about 3 versions out by that point).  I've created two enhancement issues on Github for the work:


* Compile for Cordova 2.8.0 -> https://github.com/Red-Folder/Cordova-Plugin-BackgroundService/issues/8
* Compile for Cordova 2.9.0 -> https://github.com/Red-Folder/Cordova-Plugin-BackgroundService/issues/9

Note that the source is available in my Github repositories so if you can't wait then feel free to compile yourself.  (There is some advice in the Compile for Cordova 2.9.0 issue)
## Support for getting the current timer interval
Somewhat of an oversight - currently there is no means to retrieve the timer interval.  I'd do this work under enhancement issue -> https://github.com/Red-Folder/Cordova-Plugin-BackgroundService/issues/5
## Review and re-factor of the error handling
This primarily due to this issue -> https://github.com/Red-Folder/Cordova-Plugin-BackgroundService/issues/7
An end user, through the Play Store reported an exception.  Unfortunately I don't have a way to recreate so I'll have to spend sometime dry coding the source.
I suspect I need to put some defensive code in there (certainly enough to stop the exception escaping my code).  The plugin should never throw an exception period.
## Timing
Hopefully I should have the above all dusted off by the end of August.  If nothing else, I'm going to try and get some blog posts out to inform on progress (or lack of).
Thanks for reading.

 