This is part of my automated build series.  The aim is to automate a lot of the Cordova Background Service plugin process.

In this final part of the series I will be adding two further Jenkins to make upload to my GitHub repositories nice and easy.

## Summary
I have two GitHub repositories to update:

* [https://github.com/Red-Folder/Cordova-Plugin-BackgroundService-Source](https://github.com/Red-Folder/Cordova-Plugin-BackgroundService-Source) - Source for the Plugin
* [https://github.com/Red-Folder/Cordova-Plugin-BackgroundService](https://github.com/Red-Folder/Cordova-Plugin-BackgroundService) - Pre-compile jars of the Plugin

I'm creating two further jobs.  Each will download the contents of the relevant GitHub respository then sync in any changes.

I'll add these two jobs then to the master job created in the last post.

This will leave me with two folders which I manually push up to GitHub (see below).

## Tools
For this part, I'll be using the following tools:

* Jenkins Version 1.522 (http://jenkins-ci.org/)
* Hudson Powershell plugin

## Sync Script
I only want to upload to GitHub changes I've made.  So I have a Powershell script which will run through the downloaded copy of GitHub, compare with the files created by this process and copy in any new or updated files.

Note that I have to perform specific logic for jar files.  Jars include a timestamp every time it is compiled - thus doing a file comparison will always show a difference even if the underlying code hasn't changed.  To do this, I extract the contents of the jar then test the contents.

I've saved this as a separate file to make usage easier.
%[https://gist.github.com/Red-Folder/6643225.js] 

## The job (source)
To create the job:

1) Create a new free-style project called "BackgroundServicePlugin Source Prepare For Github"
2) Tick the "This build is parameterized" and add SOURCEPATH
3) Add Git Respository under Source Code Management for "https://github.com/Red-Folder/Cordova-Plugin-BackgroundService-Source.git"
4) Add a Windows Powershell script to rename the version specific plugin from .txt to .java (I store them as .txt in my build projects to avoid conflicts):
%[https://gist.github.com/Red-Folder/6643336.js] 

5) Add a Windows Powershell script to sync up the source code changes
%[https://gist.github.com/Red-Folder/6643353.js] 

6) And save the project

## The job (compiled plugin)
To create the job:

1) Create a new free-style project called "BackgroundServicePlugin Prepare For Github"
2) Tick the "This build is parameterized" and add BUILDOUTPUTPATH
3) Add Git Respository under Source Code Management for "https://github.com/Red-Folder/Cordova-Plugin-BackgroundService.git"
4) Add a Windows Powershell script to sync up the source code changes
%[https://gist.github.com/Red-Folder/6643353.js] 

5) And save the project

## And add it into the master project
Now we add the following to the end of the "BackgroundServicePlugin Build All" job:
%[https://gist.github.com/Red-Folder/6643425.js] 

## The GitHub push
Ok.  So this gives me an automated build, test and prepare.  After about 45 minutes I've got code ready to push.

All I do then is to drop into the folders separately using Git bash and run:

%[https://gist.github.com/Red-Folder/6643484.js] 

## Wrap up
And that's it.

The automating has really helped me other the past few weeks with compiling new versions, fixing bugs and adding additional features.

If anyone has some advice on how to improve then I'd love to hear it.
