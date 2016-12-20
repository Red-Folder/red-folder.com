## Summary
This blog describes how to integrate the Android Background Service Plugin into an existing Phonegap project.

The Android Background Service Plugin will simply write out to Logcat every minute.  This simply exercise demonstration that Phonegap can start a service and that the service will continue interdependently of if the Phonegap app is closed.

## Getting Started
This blog assumes that you are working with the Phonegap project created in Eclipse similar to the Hello World application described [here.](http://docs.phonegap.com/en/2.0.0/guide_getting-started_android_index.md.html#Getting%20Started%20with%20Android)  It should also be noted that I am using Cordova 1.8.1 in this blog.  Where differences exist for Cordova 2.x.x I have appended relevant notes.

Download the following files from [here](https://github.com/Red-Folder/phonegap-plugins/tree/master/Android/BackgroundService):

* backgroundserviceplugin.jar (Cordova 2.x.x should use backgroundserviceplugin-2.0.0.jar)
* backgroundService.js (Cordova 2.x.x should use backgroundService-2.0.0.js)
* MyService.java
* myService.js (Cordova 2.x.x should use myService-2.0.0.js)
* index.html (Cordova 2.x.x should use index-2.0.0.html)

## Installing into your project

### Installing the Core Plugin
The Core Plugin are files and configuration that will be needed for any implementation that uses the Plugin.

1) Cordova 1.8.1 > Copy the backgroundserviceplugin.jar to /libs
    or
    Cordova 2.x.x > Copy the backgroundserviceplugin-2.0.0.jar to /libs.

2) Refresh the project (right click project and refresh) to ensure that the jar is visible in the library.

![Image](/media/blog/phonegap-android-background-service/image-1.png)

3) Ensure that the jar is listed in the Build Path for the project.  Right click on the /libs folder and go to Build Paths/ Configure Build Paths.  Then in the Libraries tab, add the jar library to the project.

![Image](/media/blog/phonegap-android-background-service/image-2.png)

4) Cordova 1.8.1 > Copy the backgroundService.js into assets/www.
    or
    Cordova 2.x.x > Copy the backgroundService-2.0.0 into assets/www.

5) Refresh the project to ensure that the js file is visible in the folder.

![Image](/media/blog/phonegap-android-background-service/image-3.png)

6) Cordova 1.8,1 > Add the following line to res/xml/plugins.xml
    or
    Cordova 2.x.x > Add the following line to res/xml/config.xml

%[https://gist.github.com/3778380.js]

7) Ensure that following are in the AndroidManifest.xml.  This line sets permission so that the Background Service can be restarted on device boot.

%[https://gist.github.com/3778392.js]8) 

Add the BootReceiver detail to the AndroidManifest.xml.  This should be added within the Application node.

%[https://gist.github.com/3778398.js]9) 

Your manifest file should look similar to the below:

![Image](/media/blog/phonegap-android-background-service/image-4.png)

This completes the steps to install the core Plugin.  The next steps are for specific Background Service.

### Install the Background Service 
The files and setup for a Background Service will be unique to the service.  In this instance we are putting in MyService which will output a hello message to the LogCat.

1) Create a Package called "com.yournamespace.yourappname" (if you don't already have)

2) Copy the MyService.java file into src\com\yournamespace\yourappname folder (and refresh the project):
![Image](/media/blog/phonegap-android-background-service/image-5.png)


3) Cordova 1.81 > Copy the myService.js and index.html files into assets\www folder:
     or
     Cordova 2.x.x > Copy the myService-2.0.0.js and index-2.0.0.js into assets\www folder:
![Image](/media/blog/phonegap-android-background-service/image-6.png)


4) Add the following lines to the AndroidManifest.xml.  This should be added within the Application node:
%[https://gist.github.com/3778404.js] 

5) Your manifest file should look similar to the below: 
![Image](/media/blog/phonegap-android-background-service/image-7.png)

6) Cordova 2.0.0 only > Update the MainActivity to load index-2.0.0.html rather than index.html.

The application is now good to go.

## Running the application
Running the application should give you the following screen initially:![Image](/media/blog/phonegap-android-background-service/image-8.png)

From here you can:

* Start &amp; Stop the Background Service.
* Start &amp; Stop the Timer.  Note that the MyService is set to run every minute, but will only write to the LogCat when the Timer is enabled.
* Register/ De-register for Boot.  If registered for boot, the service will automatically restart when the device is restarted,  If not registered, then you will need to run the app again to start the background service.
* Update the Config.  In this simple example, you can set the "Hello To" property of the service.
* Refresh the latest result - displays the latest result to screen.

When the service is running (and the timer is enabled), you should get messages similar to the below in the LogCat:
![Image](/media/blog/phonegap-android-background-service/image-9.png)


## Next Steps
In coming blogs I shall explain the Background Service Plugin in more detail as well a take you through the steps of extending the Plugin yourself.

I really hope this helps.  I look forward to comments. 
