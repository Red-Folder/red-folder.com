This is part 1 of a series following my exploits to automated a lot of the Cordova Background Service plugin process.

In this first post, I shall be giving an overview of the desired result, why I'm doing it and hopefully how I'll achieve.

## Motivation
I give an amount of my time to maintain and develop the Cordova Background Service plugin.  That is time I could be spending with my family, reading or going for a run (yeah right) - as such I'm quite keen to make sure that the time I do spend on it is as beneficial as possible.

The biggest time stealer with the plugin is building and testing against the various Cordova versions.  Currently I have support for about 8 versions.  So if I make 1 small change, I'm likely to easily spend twenty minutes per version.  And with a new version every month (or so) it's only going to take longer.

So as you can see it's a huge time commitment every time I make the smallest change.

## How to get my life back
There are some great tools out there to automate some of my manual processes.  I hope to use some of these to avoid sitting over my laptop for hours and hours doing the build and test process.

It is probably worth mentioning at this point that I'm not looking to save time in the end to end process.  The process I hope to build will be something that I can leave running overnight - picking up the results the next morning.  For a full time development team, my solution may not be efficient enough for the standard working day - but hopefully this will give you a good place to start.

## Feedback appreciated
I have no idea if this is the best way of achieving my desired results.  I'd be more than happy to hear any feedback if it means it make my life easier ;)

## What I want to automate
I maintain a separate SVN repository for my plugin.  On commit of code changes into that that repository I want my solution to perform the following:

* Compile the source into library (jar) for each target Cordova version 
* Test each version of the library
* Prepare content (jar, java, js, html, etc) for manual deployment to the Github repositories

Note 1: Even with automation, I'm unlikely to continue to support historic Cordova versions.

Note 2: I plan of continuing to manually deploy to Github at this stage.  This is firstly to allow me validate the build and test results, and secondly to merge any changes that I might have PULLed into my github repository (especially as my primary dev source is my SVN repository).

## Summary of the solution
Here is the current plan (at least it is today);

Use a Jenkins job to get the latest source code from SVN.  For each Cordova version;

* Build a Cordova version specific library (jar)
* Store the resulting jar for later use
* Use a Jenkins job to test the library for each Cordova version;
* Build a Cordova version specific Android application - using the jar built in the first step
* Run a Jasmine** testing script against the Android application to test the Background Service plugin basic functionality.  
* Make the test results available to Jenkins to drive workflow.
* Prepare content ready for manual upload to github

Jenkins is a Continuous Integration platform.  I'll use this to automate the process.

Jasmine is a BDD framework.  This framework allows me to test Javascript - in this case a simple Background Service.

## So whats next?
At this stage I'm not quite sure how many parts I will be producing for this series - probably in the region of a further 5;

* Part 2 - The build job - this will build an individual version of the library
* Part 3 - The testing basics - I'll need some custom tools and processes to test the library.  Given the complexity I'll blog this as a separate article.
* Part 4 - The testing job - this will use the testing tools to test an individual version
* Part 5 - The master job - this will control the full process and make sure that the build and testing jobs are coordinated and run for each target Cordova version.
* Part 6 - The output job - this will prepare and content ready for manual upload to github

Given the complexity of part 2, I wouldn't be in the least surprised if I need an extra article or two - but time will tell.
