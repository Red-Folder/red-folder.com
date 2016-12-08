This is part of my automated build series.  The aim is to automate a lot of the Cordova Background Service plugin process.

I'll need some custom tools and processes to test the library.  In this post I'll blog the reason why and the tools to use.## Summary
I want to be able to implement an automate tests of my Background Service plugin into my Jenkins CI process.  To do this I want to be able to run some JavaScript tests and report that success back to the Jenkins program.
As my Background Service plugin exposes itself itself as a JavaScript library, I want to be able to test that the JavaScript methods operate as I would expect.  To do this I will be creating tests using the Jasmine BDD framework.  The Jasmine BDD framework is specifically built to allow you to automate JavaScript testing and is easy to integrate into your web (or this case Cordova) project.  I will in the next post go into further detail of the tests I am running and how I'm integrating it.  In this post however I will concentrate on getting the results of those tests reported to Jenkins.  I would recommend that you familiarize yourself with Jasmine first by going to https://github.com/pivotal/jasmine/downloads.
To facilitate this I have created two components:
* A reporter for use with the Jasmine BDD framework. This reporter sends the results via REST to a receiver (Windows Console application).
* A receiver that has been written to be used with Jenkins CI to automate the testing of an Android application (although I'm sure it can be used for more). The receiver is used by Jenkins to wait for the Jasmine tests to be run. Jasmine, using the REST reporter, will send the results of each spec and suite to the receiver. The receiver will error (returns an error code to Jenkins) if any of the tests fail, none of the tests run or after a timeout period (5 minutes). Jenkins will then act of the result code.

All of this is available in this repository: [https://github.com/Red-Folder/Jasmine-REST-Reporter](https://github.com/Red-Folder/Jasmine-REST-Reporter)<h2>
</h2>## Hard coding
At the time of writing all of the URL details are hard coded. I've provided notes below on what to watch for.

Words of wisdom: Because this uses hardcoded details - be mindful of IP/ computer name changes.  I spend an hour debugging this only to finally realise that my laptop had picked up a new DHCP address.

## Tools used

* Jasmine BDD (https://github.com/pivotal/jasmine)
* Visual Studio 2012 Express for Windows Desktop (its free)
* Jasmine REST Reporter (https://github.com/Red-Folder/Jasmine-REST-Reporter/blob/master/jasmine_rest_reporter.js)
* Windows Application Console receiver - made up of:

* https://github.com/Red-Folder/Jasmine-REST-Reporter/blob/master/Program.cs
* https://github.com/Red-Folder/Jasmine-REST-Reporter/blob/master/LoggerController.cs
* https://github.com/Red-Folder/Jasmine-REST-Reporter/blob/master/FormatterConfig.cs


<h2>
</h2>## How to use the tools
<h3>
</h3>### The Jasmine bit
The jasmine_rest_reporter.js is used within your Jasmine tests. Using the example project from Jasmine site (https://github.com/pivotal/jasmine/downloads);
1) Copy the jasmine_rest_reporter.js into your project 2) Copy the jQuery library into your project (I'm using jquery-1.10.2.min.js). The jasmine_rest_reporter.js uses jQuery for the AJAX functionality. 3) Amend the PluginSpecRunner.html to include script links to those two .js files 4) Amend the PluginSpecRunner.html to include the following lines just after the htmlReporter is added to the jasmineEnv:
  var restReporter = new jasmine.RESTReporter();  jasmineEnv.addReporter(restReporter);
NOTE: jasmine_rest_reporter.js is hard coded to a specific URL. This will need to amended for your environment.<h3>
</h3>### The receiver bit
The receiver provides the REST service that the reporter communicates it's results to.
It's a fairly basic application - it will print to the console the details it receives from the reporter. It will also exit with an error code if any of the tests fail, none of the tests run or after a timeout period (5 minutes).
You can build the receiver using Visual Studio 2012 Express for Windows Desktop (or greater).
1) Create a new console application2) Replace the Program.cs with copy from GitHub 3) Copy in the FormatterConfig.cs and LoggerController.cs from GitHub into the project 4) Use Nuget to add the following packages:
* Json.NET
* Microsort ASP.NET Web API Self Host
* WebApiContrib.Formatting.Jsonp

This should give you a Console Application ready to use
NOTE: The Receiver is hard coded to a specific URL (see program.cs). This will need to be amended for your environment.<h3>
</h3>### The permissions bit
Windows needs to be told that the Console Application is allowed to listen on the given port. To do this run the following command:
netsh http add urlacl url=http://+:8181/ user=machine\username
where machine\username is your user account.
NOTE: If you change the port that you run the Console Application on, then this will need to be reflected in the above command.<h2>
</h2>## Wrap Up
The above is all prep for the next Part of this series - that article will go through how to use the above in a practical situation.
