I've recently been working to convert my background service plugin over to Plugman format.  In doing so I've upgraded to Cordova 3.3.0 - and suddenly the tried and tested HelloWorld project fails to work in Eclipse.

This article describes how I get round this.

## The problem
This comes down to a change the project team made in how the Cordova library code is compiled in.  Within the 3.3.0 release, they changed it from a jar to included source code.  The actual change is referenced here: https://issues.apache.org/jira/browse/CB-5232

In my searching, I found this post: http://stackoverflow.com/questions/20659853/cordova-ant-jar-not-available

This recommends importing the HelloWorld AND HelloWorld-CordovaLib project that Eclipse will find within platform/Android.  However I've been unable to make this work.  Eclipse just doesn't recognize the sub project correctly.

## The Solution
I've found this works for me.  I'd be interested to see if there is a better way of doing.

After creating a new project:

* cordova create hello com.example.hello "HelloWorld"
* cd hello
* cordova platform add android
* cordova build

I open Eclipse, and select File -> New -> Other -> Android Project from Existing Code.
![ImportProject](/media/blog/cordova-330-cordovalib-and-eclipse/ImportProject.png)

Within the dialog, select the platforms\android folder of the HelloWorld project.  I select ONLY the HelloWorld project and tick the Copy projects into workspace and click on the Finish.

I then right click the project and select Properties -> Java Build Path -> Source and Add Folder to include the CordovaLib\src folder
![ImportProject](/media/blog/cordova-330-cordovalib-and-eclipse/SourceFolder.png)

I can then build the project.
