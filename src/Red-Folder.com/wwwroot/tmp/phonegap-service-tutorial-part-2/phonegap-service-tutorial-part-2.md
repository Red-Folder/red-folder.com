## Summary
This blog is a continuation of the Service Tutorial.

In [Part 1](/blog/phonegap-service-tutorial-part-1) we created a simple application using Phonegap.  In this part we will add the code for our skeleton service.

## Versioning
As per Part 1 of this tutorial, I am using Cordova 1.8.1.  Any changes for Cordova 2.x.x have been annotated.

## Copy in the required files
Download the Plugin files and add the following to the project:

* backgroundserviceplugin.jar (Cordova 2.x.x > backgroundserviceplugin-2.0.0.jar)
* backgroundService.js (Cordova 2.x.x > backgroundService-2.0.0.js)

Instructions on downloading these file (and where to put them in you project) can be found in this [blog.](/blog/phonegap-android-background-service)

## Create the Service Class
Add a new class within the src\com.red_folder.sample folder.  Set the Name of the class to "TwitterService" and the superclass to "com.red_folder.phonegap.plugin.backgroundservice.BackgroundService":
![Image](/media/blog/phonegap-service-tutorial-part-2/Image1.png)

If you have the Inherited abstract methods, your class should look like this:
%[https://gist.github.com/Red-Folder/3778201.js] 

If Eclipse hasn't automatically created the abstract methods, then copy them in now.  

Let's go over the abstract methods:

* doWork() - This is the main class that we will be overriding.  This class gets called every time that the service timer executes.  The method returns a JSONObject which is made available, via the Plugin, to your Phonegap application.
* get/ setConfig() - These methods can be used to share configuration between your Phonegap application and the background service.  We will not be using these methods in this tutorial.
* onTimerEnabled/ Disabled() - These methods allow you attach service specific code for when the Timer is started &amp; stopped.  Note that this is not code that is run every time that the timer is fired (use doWork for that), this fires when the Timer is enabled/ disabled as appropriate.  I've used this in the past to register for GPS location on Enable and de-register on Disable.  This means that the GPS location is always available to the doWork, but isn't running permanently when the service timer isn't enabled (which can obviously be a battery drain).  We will not be using these methods in this tutorial.
* initialiseLatestResult() - This method gives you an opportunity to set up the result object before the first doWork().  This can be useful to pass information to the Phonegap Application to let it know that the service has not yet run, thus the results are not valid.  We will not be using this method is this tutorial.

We are going to add two lines to this class (which is all we will need for this part):
%[https://gist.github.com/Red-Folder/3778207.js] 

This just gives us a tag to use in Log calls.

Now lets log a message, update doWork() with a log message:
%[https://gist.github.com/Red-Folder/3778212.js] 

This completes the work on the class for this part of the tutorial.  The rest of this blog will describe plumbing the class in to your Phonegap application.

## Adding the Service into AndroidManifest.xml
Add the following to your AndroidManifest.xml.  It should be placed within the Application node.
%[https://gist.github.com/Red-Folder/3778222.js] 

For a Background Service to be available to an Android Application it must be registered within the manifest.  If you are creating an Application for multiple Background Service, you need to create a Service entry for each one.

## Add the Plugin to the Cordova configuration xml
Add the following line to res/xml/plugins.xml (for Cordova 2.x.x, add to res/xml/config.xml):
%[https://gist.github.com/Red-Folder/3778228.js] 

This registers the Plugin for use by Cordova/ Phonegap.  Note that we are registering the Plugin, which is generic regardless of how many Background Services your application may have.

## Create the javascript interface
We now create a javascript interface.

Create a new text file of "twitterService.js" within assets\www, and copy in the following code:

Cordova 1.8.1: 
%[https://gist.github.com/Red-Folder/3778237.js] 

Cordova 2.x.x:
%[https://gist.github.com/Red-Folder/3813784.js]  

This provides a javascript interface for our html page.  You will need to call the addPlugin/ define line for every Background Service you create. 

## Plumbing into index.html
And now we plumb our service into our index.html.  Copy the following code into the head section of index.html:

Cordova 1.8.1: 
%[https://gist.github.com/Red-Folder/3778262.js]

Cordova 2.x.x: 
%[https://gist.github.com/Red-Folder/3813829.js]

This javascript is a fairly simple version of what would be required for production use, but it demonstrates how to start up the Service.

Phonegap plugins have a fairly standard mechanism of providing callback functions for success and error.  In this example, we waterfall through the functions to start the service (if not already started) and enable the timer (if not already enabled).

The Background Service must be started before you can perform many functions.  Once the service is started, you need to explicitly enable the timer.  The enableTimer method takes an extra parameter which is the timer interval in milliseconds - in this case we are setting it to 60 seconds.

You should now be able to finally re-compile and run the application.  If all works, you should see the logcat message every 60 seconds:
![Image](/media/blog/phonegap-service-tutorial-part-2/Image2.png)

If you close the Phonegap Application, you will continue to see the logcat message every minute.  You should be able to start and stop the Application and the logcat will continue.

The only time that logcat will stop is if you either stop the service (via Settings) or if you reboot the device (or emulator).## Handing device reboot
Most of the times we will want our Background Service to automatically restart when our device is rebooted.

To do this we need to add the following to the AndroidManifest.xml:
%[https://gist.github.com/Red-Folder/3778275.js]

Add this within the Application node.  This block only needs to be added once regardless of the number of Background Services in your application.  This code tells Android that on boot, to call the special plugin BootReceiver class which will automatically start any Background Service registered for boot start.

We now just need to amend our javascript within index.html to call the registerForBootStart method.  The below code replaces the existing enableTimer function:

Cordova 1.8.1: 
%[https://gist.github.com/Red-Folder/3778283.js]

Cordova 2.x.x: 
%[https://gist.github.com/Red-Folder/3813846.js]

The logcat messages should now survive device reboot. 

## Next steps 
This concludes this part of the tutorial.  In the third and final part we take the skeleton service we created and build some meaningful functionality. 
