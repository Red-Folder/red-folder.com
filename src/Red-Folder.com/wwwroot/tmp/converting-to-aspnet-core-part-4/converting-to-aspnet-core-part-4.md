This is the fourth in my series of converting my red-folder.com site over to ASP.Net Core &amp; MVC 6.

The [first article](http://red-folder.blogspot.co.uk/2016/03/converting-to-aspnet-core-part-1.html) looked at creating an empty project.

The [second article](http://red-folder.blogspot.co.uk/2016/03/converting-to-aspnet-core-part-2.html) looked at copying my existing content into place and getting it to run.

The [third article](http://red-folder.blogspot.co.uk/2016/04/converting-to-aspnet-core-part-3.html) look at getting it up into Azure.

In this article, I wanted to take a deeper dive into the Gulp pipeline.
## Summary
I've really enjoyed taking a deep dive into Gulp.  It isn't a tool that I'm used to, neither are a number of the plugins that it provides - so a nice learning curve (and thus why its taken a while)

Gulp, like JavaScript in general, does suffer from a lot of options - there seem to be an unlimited number of plugins you can be using - and that's without coding anything yourself.

I suspect that over time I will adapt the plugins that I've used in my pipeline, but for now it seems to be working well.

This article is based on various commits up to and including this [commit](https://github.com/Red-Folder/red-folder.com/commit/46e0af062a94408da4016e6913ee0cd666323d07).
## Gulp Tasks
Lets start with a summary of the gulp tasks I've defined (these can all be found in gulpfile.js);


* validate-less - this task uses the gulp-lesshint module to identify any problems with my less files (note that I have a whole bunch I need to clean up - but none concern me greatly)
* compile-less - this task converts the less files to css
* autoprefix-css - this task runs through my css files and applies any prefixes that are required to support the last 2 versions of browsers > %5 market share (all done via the plugin)
* inject-css - this task will inject the development css link elements into the relevant html pages.  This saves having to add them manually.  Within the html, I inject these into Development environment blocks (see last article).  I then use the deploy-css (see below) to inject the bundled/ minified css links into Production environment blocks
* bundle-css - this task minifies, bundles and then versions the css.  By versioning, I can keep older css without fear of caching incompatibilities
* deploy-css - this tasks performs the same actions as the inject-css, just with the production css file (minified, bundled &amp; versioned) produced by the bundle-css
* validate-js - this task uses jscs (JavaScript Coding Styles) and JsHint to advise on problems in the JavaScript
* inject-bower - this task reads the bower dependencies and injects css links/ js script blocks into the relevant html files.  It should save manually have to copy them in
* inject-js - same as the inject-css, but for the JavaScript
* bundle-js - again same as the css version, but for the JavaScript
* deploy-js - again same as the css version, but for the JavaScript
* deployment-prepare - task to be run when the project is built to ensure that everything is production ready (see more on this below)
* watch-less - task that watches for .less changes - on change will kick off the validate-css, compile-css, autoprefix-css &amp; inject-css tasks
* watch-js - task that watches for .js changes - on change will kick off the validate-js &amp; inject-js tasks
* validate-gulp - a helper task to validate the JavaScript in the gulpfile.js and its supporting gulp.config.js

## Gulp.Config.js
While the gulpfile.js contains the logic, the gulp.config.js contains the instructions.  This is arguably where all the important stuff happens.  Get the instructions right, and the rest is actually fairly easy.
After various experiments, I've adopted a common <i>app</i> structure for my css &amp; JavaScript.  Each app will have a named folder (one within scripts, one within css) and will have a name.  That name is used for injection purposes and forms part of the injection tag found in the relevant html files.
All of the less/ css/ js files go into their relevant folder.  I then have a 3rdparty directory off each for any 3rd party dependencies.  I do this so that I know which ones I want to validate (no point doing 3rd party ones).
Against the app I also have which html file(s) that the app will be injected into.
Once I have that app, I then have a variety of helper functions to transform the data into a meaningful config for each of the relevant gulp tasks.
For example, jsToValidate will return all the js that I want to validate per app (which will basically be everything except the 3rd party stuff).  The gulp tasks will call the relevant function and away they go - thus I'm able to get a fairly consistent structure to my gulp tasks.
As I move forward and look at "real" JavaScript applications, this setup should make them easy to add as just another app.
You'll also notice that within each app folder (css &amp; script) I have a production folder.  This will be where the minified, bundled, versioned files end up.  Unlike many demos, I don't delete the old ones.  This allows cached pages to continue to work.  I'm considering these files immutable.## Wiring it up to project build
One thing that took a while to understand was how to wire up the deployment-prepare task to run on a release build AND only on a release build.
Because the deployment-prepare will, by virtue of calling deploy-css &amp; deploy-js, create immutable files - ones which I'm unlikely to archive (well not often anyway) - I only wanted to generate them on release build.
If I generated on debug build, then I'd quickly have hundreds if not thousands of them which should never be released into production.
To accomplish this, you need to set up the project to call gulp and pass in the build configuration.
Within the project.json you have scripts bock, in which I added:
"prebuild": [ "gulp deployment-prepare --%build:Configuration%" ]
Simple enough - it calls gulp with the deployment-prepare task - but will also pass in the build configuration as a command line argument - either --Debug or --Release

Within the deployment-prepare gulp task, I simple grab the command line arguments and look for the --Release.  If there then kick off the deploy-css &amp; deploy-js.

Easy once you know how.
## Lessons learnt
There are a lot of Gulp libraries - pick one that does what you need.  If you try to compare the various alternatives you'll never use it.

The task dependencies will be run in parallel.  If you look at the task definitions, some will have dependencies that need to be run.  For example, if you look inject-css, I specify the dependencies as:

['compile-less', 'autoprefix-css']
Gulp will run these in parallel rather than series as you might expect (I did).  So you can end up with a case where the autoprefix is being performed on a file which is then overwritten by the compile-less.

Easy enough to get round by setting all the appropriate dependencies.

In the next version of Gulp (v4), this method of dependencies is removed and you explicitly use parallel and series calls.  This should be a lot simpler to understand.  There is a good write up on it [here](https://fettblog.eu/gulp-4-parallel-and-series/).
## Further steps
Both the gulpfile.js or gulp.config.js would benefit from refactoring work.
The gulpfile.js has a lot of duplicated code - I should be able to make considerably cleaner.
The gulp.config.js can be made a lot simpler.  The power of my pipeline is getting the right information from the config object in the right way - thus a lot of work has gone into producing the correct info.  I've gone through various revisions on how to produce that info - the file is showing some of those scars.
I should also look at cleaning up the Less warnings.  Seems a little pointless reporting on this if nothing is to be done with them.## Next
Next I'd like to have a look at WebApi - this will be a pre-cursor to provide a very basic level of data for a JavaScript app (probably Angular).

The Gulp pipeline.  Fourth in my series of converting my red-folder.com site over to ASP.Net Core &amp; MVC 6.
Red Folder Consultancy Ltd
<img itemprop='image' src='http://www.red-folder.com/images/blog/AspNetCoreSeries.png'/>
