This is part of my automated build series.  The aim is to automate a lot of the Cordova Background Service plugin process.

In this blog I'll use the tools I created in the previous part to build a Jenkins job that tests the plugin.

## Summary
In much the same way as I have a parameterized build job (see [Part 2](http://red-folder.blogspot.co.uk/2013/08/automated-build-part-2-build-job_11.htmlPart%202)), I also want a parameterized test job.  The test job will, for each version of the Background Service plugin, run through some tests (using Jasmine and the tools I build in the [last post](http://red-folder.blogspot.co.uk/2013/08/automated-build-part-3-testing-basics.html)) and output files ready to be pushed up to Github.

## Source files
I have a test project setup in my SVN.  The test project is an Android/ Cordova project which is set up with the MyService I provide as a sample in Github.

Within the assets/www of the project I have a sub directory for each version which contains:

* Version specific cordova-x.x.x.js
* Version specific backgroundService-x.x.x.js
* Version specific myService-x.x.x.js

I also, have with the assets a Jasmine area which is configured to use my REST reporter as described last post and a version of the PluginSpecRunner.html which expects the following files:

* assets/www/Jasmine/src/backgroundService.js
* assets/www/Jasmine/src/cordova.js
* assets/www/Jasmine/src/myService.js

As you will see later in job steps, for the version under test, I copy the version specific files into the src folder (renaming them).

The Cordova app is automatically set to run the PluginSpecRunner.html on start.

## Tools
For this part, I'll be using the following tools:

* Jenkins Version 1.522 (http://jenkins-ci.org/)
* Ant Plugin
* Jenkins Subversion Plug-in
* Hudson Powershell plugin
* Apache Ant 1.8.3
* Android SDK
* Java JDK 1.6.0
* Jasmine BDD (https://github.com/pivotal/jasmine)
* Jasmine REST Reporter (See [last post](http://red-folder.blogspot.co.uk/2013/08/automated-build-part-3-testing-basics.html))
* Windows Application Console receiver (See [last post](http://red-folder.blogspot.co.uk/2013/08/automated-build-part-3-testing-basics.html))

## The testing bit
I wanted to separate the testing logic out into this separate section.  As mentioned I'm using the Jasmine BDD framework to test my JavaScript interface.  Jasmine uses a "spec" definition.  At a high level the test will:

* If the Service isn't running, then startService should start it
* If the Service is running, then enableTimer should enable the timer
* If the Service is running and the timer is enabled, then we should get a different result every 2 minutes
* If the Service is running and the timer is enabled, then disableTimer should disable the timer
* If the Service is running, then stopService should stop the service

Note: My current "spec" doesn't test all of the Plugin or Background Service functionality.  I will expand this over time, but the above are the key areas that I want to focus on at this time.

Easiest way to explain what I'm doing is just document the code ... so here goes:
%[https://gist.github.com/Red-Folder/6252108.js]

## Variables
The Jenkins job will be parameterized to allow it to be called repeatedly for different versions.

As such the following variables will defined in the job:

* PLUGINVERSION - This is the version of the library that will be created.  I use the variable to pull in the correct version of the BackgroundServicePlugin.java for the library.  I also use the PLUGINVERSION to name the resulting jar.
* CORDOVAVERSION - This is the version of the Cordova library to be used in the build.  As this can sometimes differ from the PLUGINVERSION I'm using a separate variable.  Most of the time these will be the same.
* BUILDOUTPUTPATH - A path to copy the resulting files.  The files will be used later in the full process so needs to be stored outside of the Jenkins environment.

## The job
To create the job;

1. Create a new free-style software project in Jenkins, naming it "BackgroundServicePlugin BDD Test"
2. Tick the "This build is parameterized" and provide the above 3 variables
3. Setup the SVN details within the Source Code Management (note that I do not set up any build triggers)
4. Under Build Environment, enable "Run an Android emulator during build":
![Image](/media/blog/automated-build-part-4-testing-job/BuildEnv.png)

[UPDATE]: Version 3 of Cordova needs Android OS 2.2 as a minimum.  As such I have since changes the above Android OS version.  I also tick the "Use emulator snapshots" as it speeds the whole process up.

5. Add a Windows Batch command to copy the relevant libaries into the source and set up the correct js file for Jasmine:
%[https://gist.github.com/Red-Folder/6252570.js] 

6. Add a Windows Powershell script to update the .classpath file with the relevant versions of the Cordova and BackgroundService libraries.
%[https://gist.github.com/Red-Folder/6642733.js] 

7. Add a Windows Powershell script to load in a Cordova version specific res\xml\config.xml.  I've needed to do this because of the difference in structure used between 2.x.x and 3.x.x.  I store the is res\xml\x.x.x\config.xml where x.x.x is the Cordova version number.
%[https://gist.github.com/Red-Folder/6642746.js]

8. Add a Windows Batch command to prepare the project:
%[https://gist.github.com/Red-Folder/6252577.js] 

9. Invoke Ant to actually build the app
%[https://gist.github.com/Red-Folder/6252580.js] 

10. Add a Windows batch commend to run the app on the Android emulator
%[https://gist.github.com/Red-Folder/6252587.js] 

11. Add a Windows Powershell script to check that the app is running
%[https://gist.github.com/Red-Folder/6252594.js] 

12. Run the JasmineReceiver.exe created in the [last post](http://red-folder.blogspot.co.uk/2013/08/automated-build-part-3-testing-basics.html).  Note that the Jenkins job will wait for the console application to complete before proceeding to the next step.  The console application will close either when the timeout has exceeded or all of the test results have been received from the app running on the Android emulator.
%[https://gist.github.com/Red-Folder/6252599.js] 

13. Add a Windows batch command to close the app on the Android emulator
%[https://gist.github.com/Red-Folder/6252604.js] 

12. Add a Windows Powershell script to check that app has stopped
%[https://gist.github.com/Red-Folder/6252606.js] 

13. Finally a script to copy the relevant files for later use
%[https://gist.github.com/Red-Folder/6252651.js] 

And that's it.  Should any of the steps fail (build, install, etc) then the whole job will fail.

## Wrap up
There is quite a lot in the above, but hopefully it makes sense.  If not please add a comment and I'll answer to the best of my abilities.

Next step, running the overall job (calling the build and test for each version).
