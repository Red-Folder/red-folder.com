## Summary
In this final part of this tutorial we shall complete the TwitterService.

The Service shall use the Twitter webservice to get the latest "Max ID".  If that Max ID is different from the last time the service ran then it will post a notification to the status bar.

## Versioning
As per Part 1 &amp; 2 of this tutorial, it is written using Cordova 1.8.1.  Where any differences of Cordova 2.x.x exist I have annotated.## Checking Twitter 
Most of the work in this Tutorial is fleshing out the TwitterService.java.  To begin with, lets add some imports to the file so that it includes there (which will all be required later in the class):

%[https://gist.github.com/3777973.js] Now we need somewhere to store the Max ID.  For this add the following get/ set: %[https://gist.github.com/3778149.js]I'm using Android Shared Preferences to store the Max ID.  Using the Shared Preferences allows use to store the Max ID between device reboot or service restart.  The getMaxID() uses the Preference Manager to get the value of the "MaxID" key (defaulting to an empty string if it doesn't have one).  The setMaxID() again uses the Preference Manager, puts it into edit mode and saves the passed value against the "MaxID" key. 

Now for the heavy lifting of the class, the newTweet() method.  Simply enough this will return true if a new Tweet has been found:
%[https://gist.github.com/3778161.js] Ok, so the method starts by call Twitter to get the latest "Phonegap" Tweet as a JSON string.  The string is then converted to a JSONObject.  We then grab the Max ID from the JSON Object and compare to the last one received.  If it's different then we store the Max ID (using setMaxID) and return true.  Else we are returning false.

Now we just wire the newTweet into the doWork().  Simply replace the existing doWork method with the following:
%[https://gist.github.com/3778167.js] Ok, we've reached a point that we can run it.  There are two messages you should see, first a logcat if there is  a new tweet:
![Image](/media/blog/phonegap-service-tutorial-part-3/image2.png)


And then a logcat if there isn't:

![Image](/media/blog/phonegap-service-tutorial-part-3/image3.png)
## Status Notification Icon 
We now need to create and add the notification icons that appears in the status bar.  For the purpose of this tutorial I recommend using a simple generator that can be found [here](http://android-ui-utils.googlecode.com/hg/asset-studio/dist/icons-notification.html#source.type=text&amp;source.space.trim=1&amp;source.space.pad=0&amp;source.text.text=RF&amp;source.text.font=Courier%20New&amp;shape=square&amp;name=notification).  The generator allows you to quickly create icons in the relevant size for the different API versions (examples seen below).
I'm using API 8 for my project so will be using the "Older Version" icons.  Note that I've set the generator name to notification so that images are generated as ic_stat_notification.
<table>   <tbody><tr>      <th></th>      <th>Older Versions</th>      <th>API 9</th>      <th>API 11</th>   </tr><tr>      <th>ldpi</th>      <td>![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bldpi-255D.png)

</td>      <td>![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bldpi-v9-255D.png)

</td>      <td style="background-color: #555555;">![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bldpi-v11-255D.png)

</td>   </tr><tr>      <th>mdpi</th>      <td>![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bmdpi-255D.png)

</td>      <td>![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bmdpi-v9-255D.png)

</td>      <td style="background-color: #555555;">![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bmpdi-v11-255D.png)

</td>   </tr><tr>      <th>hdpi</th>      <td>![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bhdpi-255D.png)

</td>      <td>![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bhdpi-v9-255D.png)

</td>      <td style="background-color: #555555;">![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bhdpi-v11-255D.png)

</td>   </tr><tr>      <th>xhdpi</th>      <td>![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bxhdpi-255D.png)

</td>      <td>![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bxhdpi-v9-255D.png)

</td>      <td style="background-color: #555555;">![Image](/media/blog/phonegap-service-tutorial-part-3/ic_stat_notification-255Bxhdpi-v11-255D.png)

</td>   </tr></tbody></table>The relevant version of icons then need to be copied into the relevant res folders.  Once done (and you've refreshed the Eclipse project) you should have something similar to the below:

![Image](/media/blog/phonegap-service-tutorial-part-3/image1.png)

For more information on Status Bar Icons, see the [Android developer guidelines.](http://developer.android.com/guide/practices/ui_guidelines/icon_design_status_bar.html)

## Create a status notification 
Now lets add the notification code:
%[https://gist.github.com/3778177.js] The showNotification() method accepts a title and some text to put into the notification.  The key to this method is the creation of the new Intent object passing across the TwitterExampleActivity.class.  This tells Android that when the user clicks on the notification, that it should open up our Twitter application.

It is possible to pass extra information as part of the notification - so for example you could pass the specific Tweet ID, which our application would receive and display a specific page.  This is however outside of the scope of this tutorial.

Now we simply write it into the doWork() (yet again we replace the existing method):
%[https://gist.github.com/3778184.js] Simple enough, it just runs the showNotification if a new Tweet is found.

Install and run the application.  Close the application once the alert dialog has fired.  Then sit back and wait.

Eventually you should get a new Tweet, and the notification will display in the status bar:
![Image](/media/blog/phonegap-service-tutorial-part-3/image4.png)



Pull the status bar down to see the notification details:

![Image](/media/blog/phonegap-service-tutorial-part-3/image5.png)

Click on the notification and our application will be opened:

![Image](/media/blog/phonegap-service-tutorial-part-3/image6.png)
## 
## Full Code
The full code for this example can be found:


* [Here for Cordova 1.8.1](https://github.com/Red-Folder/phonegap-samples/tree/master/1.8.1/TwitterExample)
* [Here for Cordova 2.x.x](https://github.com/Red-Folder/phonegap-samples/tree/master/2.0.0/TwitterExample)

## Job done
And that's all folks.
There is plenty that I would do to the service to make it production ready, but hopefully I have given you enough to get started.
I'd love you here about how you get on. 