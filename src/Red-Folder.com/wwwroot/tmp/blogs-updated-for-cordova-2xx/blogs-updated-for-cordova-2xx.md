## Summary
I've updated my blogs for Cordova 2.x.x.

Below is a summary of the differences:

## Filenames
I've renamed a number of the files in the Plugin to make versioning a lot easier going forward.

* backgroundserviceplugin.jar is renamed to backgroundserviceplugin-2.0.0.jar
* backgroundService.js is renamed to backgroundService-2.0.0.js
* myService.js is renamed to myService-2.0.0.js
* index.html is renamed to index-2.0.0.html

## Cordova config
Previously we registered the plugin within res\xml\plugins.xml.

Within Cordova 2.x.x, this is now res\xml\config.xml

## Registering the plugin
The registering of a plugin is different under Cordova 2.x.x, and now needs to use cordova.define.

See myService-2.0.0.js as an example of how to do this.

## Changes to calling the plugin
Under Cordova 1.8.1 the service is called (from the html) via window.plugins.myService.method(xxx).

Under Cordova 2.x.x you can drop the window.plugins so that it is just myService.method(xxx).<b></b> 
