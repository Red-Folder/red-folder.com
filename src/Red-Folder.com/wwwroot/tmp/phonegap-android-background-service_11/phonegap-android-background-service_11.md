## Summary
The blog will provide some background behind my Phonegap Background Service Plugin.

A future blog will cover how to use the Plugin to create your own service, however I would recommend reading the below so that the concepts make sense.

## What is a Background Service?
Android Applications work differently than the applications we are used to in our PC world.  On our PC's we have become used to running multiple applications and have become used to them working away in tandem.

For example, if we open a YouTube video in IE, we know that it will continue to run regardless of if we are watching it or we switch to Excel.

With Android however an Application will "run" only when it is onscreen.  Once an Application is not on screen (be it because we've taken a phone call, the screen saver comes on, or you open another application) then our original Application will be suspended.  When suspended, none of the Application code will run.

This is a problem if you have code that needs to continue running.  An example of this would be an email client were we want to check for new mail on a regular basis (regardless of if the Application is running or not).

Android provides for this by allowing developers to run Background Services.  A Background Service, as the name implies, runs in the Background.  The benefit of this for us is that we can use a Background Service to run code all the time - regardless of if our Application is running or not.

## Why does Phonegap need a Background Service Plugin?
Background Services for Android need to be developed in Java.  To be able to access that java code from Phonegap then a Plugin is required.

## The Phonegap Background Service Plugin explained
The Plugin, as available [here](https://github.com/Red-Folder/phonegap-plugins/tree/master/Android/BackgroundService), is made up of two parts:

* The core Background Service files
* Sample files on how to produce your own Background Service

The files logically break up as per this diagram:
![Image](/media/blog/phonegap-android-background-service_11/Image-1.png)

Every Phonegap Application that uses the Plugin needs to include the core Background Service files:

* backgroundService.js
* backgroundServicePlugin.java
* backgroundService.java

Each Phonegap Application will then need to have, for every Background Service, two files:

* A java class which extends the backgroundService.java.  In the above example this is the MyService.java.  This class contains the code that is run by the Background Service.
* A js file.  In the above example this is the myService.js.  This provides a simple interface over the backgroundService.js javascript for use in your html code.

## What next?
Next I will start a short series on using the Plugin to create your own Background Service. 
