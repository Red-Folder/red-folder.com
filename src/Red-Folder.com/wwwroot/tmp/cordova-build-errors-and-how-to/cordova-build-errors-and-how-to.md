I automate Jenkins &amp; Cordova CLI to automate my plugin build and test.  Occasionally I will get errors when running "cordova build".  The error messages reported from a build failure doesn't tell you why it went wrong ... so we need to do some digging.

Ok, so to start with I'm getting the following error (from Jenkins logs) when running "cordova build"
<pre><code>
C:\Program Files\Jenkins\jobs\Scheduler Plugin BDD Tests\workspace>C:\JenkinsWorkspace\CordovaCLI\node_modules\.bin\cordova build 
Generating config.xml from defaults for platform "android"
Preparing android project
Compiling app on platform "android" via command "cmd" /c C:\Program Files\Jenkins\jobs\Scheduler Plugin BDD Tests\workspace\platforms\android\cordova\build

C:\JenkinsWorkspace\CordovaCLI\node_modules\cordova\node_modules\q\q.js:126
                    throw e;
                          ^
Error: An error occurred while building the android project.
    at ChildProcess.<anonymous> (C:\JenkinsWorkspace\CordovaCLI\node_modules\cordova\src\compile.js:65:22)
    at ChildProcess.EventEmitter.emit (events.js:98:17)
    at maybeClose (child_process.js:735:16)
    at Process.ChildProcess._handle.onexit (child_process.js:802:5)
Build step 'Execute Windows batch command' marked build as failure
</anonymous></code>
</pre>This unfortunately just tells us that the build failed ... but not why.  For that we need to look deeper.

You'll see that Cordova is calling platforms/android/cordova/build.  So, I open up a node.js command prompt (which is configured to run Cordova CLI), cd into platforms/android/cordova/ and run the build.

This givens me loads more info.  I've tried to tidy this up as much as possible to make it someway readable - by default (on Windows) it will display with lots of "\r\n" (carriage return &amp; line feed) and be a fair jumble to read.  Generally you'll need to search through for BUILD FAILED which is likely to be towards the end:

<pre><code>
exec: ant clean -f "C:\Program Files\Jenkins\jobs\Scheduler Plugin BDD Tests\workspace\platforms\android\build.xml"
[ 'ant clean -f "C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\build.xml"',
  null,
  'Buildfile: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\build.xml

-check-env:
 [checkenv] Android SDK Tools Revision 22.3.0
 [checkenv] Installed at C:\\Program Files\\Android\\android-sdk\r\n\r\n-setup:
     [echo] Project Name: HelloWorld
 [gettype] Project Type: Application

-pre-clean:

clean:
   [delete] Deleting directory C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\bin
   [delete] Deleting directory C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\gen
[getlibpath] Library dependencies:
[getlibpath] 
[getlibpath] ------------------
[getlibpath] Ordered libraries:

nnodeps:

-check-env:
 [checkenv] Android SDK Tools Revision 22.3.0
 [checkenv] Installed at C:\\Program Files\\Android\\android-sdk

-setup:
     [echo] Project Name: CordovaLib
  [gettype] Project Type: Android Library

-pre-clean:

clean:
   [delete] Deleting directory C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin
  [delete] Deleting directory C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\gen

BUILD SUCCESSFUL
Total time: 1 second
',
  '' ]

exec: ant debug -f "C:\Program Files\Jenkins\jobs\Scheduler Plugin BDD Tests\workspace\platforms\android\build.xml"
[ 'ant debug -f "C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\build.xml"',
  { [Error: Command failed:  BUILD FAILED
  C:\Program Files\Android\android-sdk\tools\ant\build.xml:720: The following error occurred while executing this line:
  C:\Program Files\Android\android-sdk\tools\ant\build.xml:734: Compile failed;

see the compiler error output for details.

  Total time: 7 seconds
  ] killed: false, code: 1, signal: null },
  'Buildfile: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\build.xml

-set-mode-check:\r\n\r\n-set-debug-files:
-check-env:
 [checkenv] Android SDK Tools Revision 22.3.0
 [checkenv] Installed at C:\\Program Files\\Android\\android-sdk

-setup:

     [echo] Project Name: HelloWorld
  [gettype] Project Type: Application

-set-debug-mode:

-debug-obfuscation-check:

-pre-build:

-build-setup:
[getbuildtools] Using latest Build Tools: 18.0.1
     [echo] Resolving Build Target for HelloWorld...
[gettarget] Project Target:   Android 4.4.2
[gettarget] API level:        19     
[echo] ----------
[echo] Creating output directories if needed...    
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\bin
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\bin\\res
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\bin\\rsObj
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\bin\\rsLibs
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\gen
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\bin\\classes
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\bin\\dexedLibs
[echo] ----------
[echo] Resolving Dependencies for HelloWorld...
[dependency] Library dependencies:
[dependency] 
[dependency] ------------------
[dependency] Ordered libraries:
[dependency] 
[dependency] ------------------
[echo] ----------
[echo] Building Libraries with \'debug\'...

nnodeps:

-set-mode-check:

-set-debug-files:

-check-env:
[checkenv] Android SDK Tools Revision 22.3.0
[checkenv] Installed at C:\\Program Files\\Android\\android-sdk

-setup:

[echo] Project Name: CordovaLib
[gettype] Project Type: Android Library
-set-debug-mode:
-debug-obfuscation-check:
-pre-build:

-build-setup:
[getbuildtools] Using latest Build Tools: 18.0.1
[echo] Resolving Build Target for CordovaLib...
[gettarget] Project Target:   Android 4.4.2
[gettarget] API level:        19
[echo] ----------
[echo] Creating output directories if needed...
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\res
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\rsObj
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\rsLibs
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\gen
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\classes
[mkdir] Created dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\dexedLibs
[echo] ----------
[echo] Resolving Dependencies for CordovaLib...
[dependency] Library dependencies:
[dependency] No Libraries
[dependency] 
[dependency] ------------------

-code-gen:
[mergemanifest] Merging AndroidManifest files into one.\
[mergemanifest] Manifest merger disabled. Using project manifest only.
[echo] Handling aidl files...
[aidl] No AIDL files to compile.
[echo] ----------
[echo]
 Handling RenderScript files...
[echo] ----------
[echo] Handling Resources...
[aapt] Generating resource IDs...
[echo] ----------
[echo] Handling BuildConfig class...
[buildconfig] Generating BuildConfig class.

-pre-compile:

-compile:
    [javac] Compiling 73 source files to C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\classes
    [javac] Note: Some input files use or override a deprecated API.
    [javac] Note: Recompile with -Xlint:deprecation for details.
     [echo] Creating library output jar file...
      [jar] Building jar: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\classes.jar

-post-compile:

-obfuscate:

-dex:
     [echo] Library project: do not convert bytecode...

-crunch:
   [crunch] Crunching PNG Files in source dir: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\res
   [crunch] To destination dir:C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\res
   [crunch] Crunched 0 PNG files to updatecache

-package-resources:
     [echo] Library project: do not packageresources...

-package:
     [echo] Library project: do not package apk...

-post-package:

-do-debug:
     [echo] Library project: do not create apk...
[propertyfile] Creating new property file: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\build.prop
[propertyfile] Updating property file: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\build.prop
[propertyfile] Updating property file: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\build.prop
[propertyfile] Updating property file: C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\CordovaLib\\bin\\build.prop

-post-build:

debug:

-code-gen:
[mergemanifest] Merging AndroidManifest files into one.
[mergemanifest] Manifest merger disabled. Using project manifest only.
     [echo] Handling aidl files...
     [aidl] No AIDL files to compile.
     [echo] ----------
     [echo] Handling RenderScript files...
     [echo] ----------

   [echo] Handling Resources...
     [aapt] Generating resource IDs...
   [echo] ----------
     [echo] Handling BuildConfig class...
[buildconfig] Generating BuildConfig class.

-pre-compile:

-compile:
    [javac] Compiling 24 source files to C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\bin\\classes
    [javac] C:\\Program Files\\Jenkins\\jobs\\Scheduler Plugin BDD Tests\\workspace\\platforms\\android\\src\\com\\red_folder\\phonegap\\plugin\\scheduler\\models\\AlarmDictionary.java:44: cannot find symbol
    [javac] symbol  : method put(long,com.red_folder.phonegap.plugin.scheduler.models.Alarm)
    [javac] location: class com.red_folder.phonegap.plugin.scheduler.models.AlarmDictionary
    [javac]            result.put(tmpAlarm.getId(), tmpAlarm);
    [javac]                   ^
    [javac] 1 error
',  '
BUILD FAILED
C:\\Program Files\\Android\\android-sdk\\tools\\ant\\build.xml:720: The following error occurred while executing this line:
C:\\Program Files\\Android\\android-sdk\\tools\\ant\\build.xml:734: Compile failed; see the compiler error output for details.

Total time: 7 seconds
' ]
Error executing "ant debug -f "C:\Program Files\Jenkins\jobs\Scheduler Plugin BDD Tests\workspace\platforms\android\build.xml"":
BUILD FAILED
C:\Program Files\Android\android-sdk\tools\ant\build.xml:720: The following error occurred while executing this line:
C:\Program Files\Android\android-sdk\tools\ant\build.xml:734: Compile failed; see the compiler error output for details.

Total time: 7 seconds
</code></pre>

So in this case the error I'm getting is compile failure in the AlarmDictionary.java - this of course allows me to go dig into the code and resolve.

In this particular example, the AlarmDictionary.java is a legacy file from prior to a refactor and hasn't been removed from the build.  Remove the file and all is good.  Obviously your resolution will depend on the error you get - good luck.