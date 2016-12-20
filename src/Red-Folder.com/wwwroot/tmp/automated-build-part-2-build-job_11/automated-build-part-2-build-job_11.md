This is part of my automated build series.  The aim is to automate a lot of the Cordova Background Service plugin process.

In this post, I'll be describing the build process for an individual version of the library.

## Summary
During this post I will be demonstrating creating a Jenkins job to compile the library source into an Android library and save out the library for later use.

## Tools used
I'm using the following tools for this post:

* Jenkins Version 1.522 (http://jenkins-ci.org/)
* Ant Plugin
* Jenkins Subversion Plug-in
* Hudson Powershell plugin
* Apache Ant 1.8.3
* Android SDK
* Java JDK 1.6.0

## Sources
I have a SVN repository for my library project.  I use this repository during development.  I only post to my GitHub repository once development has been completed and tested.

Within the SVN repository, the source is stored in an Android Library project and can be compiled using Eclipse.

## Variables
The Jenkins job will be parameterized to allow it to be called repeatedly for different versions.

As such the following variables will defined in the job:

* PLUGINVERSION - This is the version of the library that will be created.  I use the variable to pull in the correct version of the BackgroundServicePlugin.java for the library.  I also use the PLUGINVERSION to name the resulting jar.
* CORDOVAVERSION - This is the version of the Cordova library to be used in the build.  As this can sometimes differ from the PLUGINVERSION I'm using a separate variable.  Most of the time these will be the same.
* BUILDOUTPUTPATH - A path to copy the resulting library jar file to.  The jars will be used later in the full process so needs to be stored outside of the Jenkins environment.
* SOURCEPATH - A path to copy the relevant source code for later uploading to the source code GitHub repository.  To keep the source "clean" I only upload the Plugin specific code rather than the entire project.

## The job
To create the job;

1. Create a new free-style software project in Jenkins, naming it "BackgroundServicePlugin Build"
2. Tick the "This build is parameterized" and provide the above 3 variables
3. Setup the SVN details within the Source Code Management (note that I do not set up any build triggers)
4. Add a Windows batch command to copy the relevant source code into my SOURCEPATH for later upload.  I also collapse the src &amp; aidl folders into one.  The Ant build doesn't seem to cope with having the aidl files in a separate source folder.  I'm sure that there is a better method of doing this (in the Ant build), but this works.
%[https://gist.github.com/Red-Folder/6642410.js]

5. Add a Windows Powershell script to update the project to use the correct Cordova Version (note, I store all of the Cordova-x.x.x.jar files within the Android Project and thus the SVN repository
%[https://gist.github.com/Red-Folder/6205767.js]
The powershell script runs through the .classpath file of the Android project and links in the appropriate Cordova jar version.

6. Add an Execute Windows batch command to copy in the correct version of the BackgroundServicePlugin.java
%[https://gist.github.com/Red-Folder/6205783.js]

7. Add an Execute Windows batch command to prepare the project
%[https://gist.github.com/Red-Folder/6205787.js]

8. Add an "Invoke Ant" to build the library
%[https://gist.github.com/Red-Folder/6205789.js]

9. Finally, add an Execute Windows batch command to copy the resulting library to the BUILDOUTPUTPATH for later use
%[https://gist.github.com/Red-Folder/6205790.js]## Wrap up

This Job now allows me to automate the build of an individual version of the Background Service Plugin library.

On it's own this job is rather boring.  It's power comes in later when combined with a controlling job which calls this job for each version that I want to build - but that comes later in part 5.
