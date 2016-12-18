To get some experience with the all the new .Net goodness coming in, I've decided to convert my www.red-folder.com site from ASP.Net MVC 5 to ASP.Net Core &amp; MVC 6.

Using the Pluralsight course [Building a Web App with ASP.NET 5, MVC 6, EF7 and AngularJS ](https://app.pluralsight.com/library/courses/aspdotnet-5-ef7-bootstrap-angular-web-app/table-of-contents) by Shawn Wildermuth as a template, I plan on utilising a good number of the upcoming features.

Note that isn't a step by step tutorial.  Shawn's course does a lot better job than I could do.  This is mean to be a quick overview of that changes to convert a fairly basic website.

The current implementation of www.red-folder.com isn't brilliant - it's hardly going to win awards.  But it gives me something to start with.

First steps will be to create a new project - this I'll host on GitHub so you can see the progress as I go.  I'm sure there is a way to migrate an existing project - but with such a simple project it makes much more sense to start from scratch and build it right.

I'll move over all the website content - taking advantage of the difference in the new tooling (Bower, tag helpers, etc).

I'll then look at splitting some of the "data" into Web API (currently I just hardcode the data in classes).  While at some point I'll split these out into separate Microservices (good excuse to finally get back to my long stalled Microservice series), initially I'll follow best practice of creating separation (DDD style) within the same codebase.  Always easier to refactor within the same codebase than across Microservice boundaries.

I'll probably use this as a good excuse to look at EF7.  Would be good to try it against Mongo as well as SQL Server - but I can't find of a reasonable use-case at this point (like that would stop me).

I'll probably also use the opportunity to add some Angular to the site as well.  Maybe try out Angular 2 as part of this as well.

However, before I get carried away, we need to start with the source repository.

So you can follow allow I've created this [here.](https://github.com/Red-Folder/red-folder.com)

## Getting started
Simple enough start, install the ASP.Net 5 RC1 from [get.asp.net](http://get.asp.net/).

(Helps if you have Visual Studio 2015 installed first).

I then use dnvm (Dot Not Version Manager) to download the latest runtime version.  In my case that is 1.0.0-rc1-update1 for x64 architecture.  I also set this to my default.

dnvm is a command line utility.  Not sure if there are plans to put a GUI over this (or indeed a need) - but I'd expect to see a lot more command line starting to drift into .Net development moving forwards.

Then start up Visual Studio and create a new project.  I used the Empty Asp.Net 5 template.  Like Shawn, I want to start with an empty project so that I can understand what I need to add to make it work.

As some point I'll probably create a project from the Asp.Net 5 Web Application template to see what it adds on top of the empty project.

You can see the initial commit [here](https://github.com/Red-Folder/red-folder.com/commit/6d8cfcfc934d7c2fc36b90e5c7b4437d5690db9c).

Ok, at this point the project does very little.

We now need to set it up for MVC.

## Configure for MVC
Ok, a fair amount of stuff to do here from the empty project.

I create the following folders:

* Controllers
* Controllers\Web (I'm following the advise of Shawn to split the controllers between Web &amp; WebApi)
* Controllers\Api
* Views
* Services
* Models
* ViewModels
* wwwroot\img (We now have a root folder for our website)
* wwwroot\css
* wwwroot\js

I then need to configure MVC within Startup.cs (see the commit for the difference).

I've also added the following:

* Views\_ViewImports.cshtml - to import the Tag Helpers (see more in the next article)
* "Microsoft.AspNet.Mvc.TagHelpers": "6.0.0-rc1-final" to the project.json dependencies

## Installing JavaScript libraries
Ok, so rather than using Nuget for JavaScript libraries we now use Bower.

Bower is a JavaScript client side package loader.

(You still use Nuget for server side packages)

You add a new project item - Bower Configuration File (you'll find it under the client-side templates).  This is the bower.json file.

Within the bower.json, I add the dependencies I need:

* "jquery": "~2.2.2"
* "bootstrap": "~3.3.6"
* "font-awesome": "4.4.0"
* "bootswatch": "~3.3.6"

Bower will download the file and place them into a wwwroot\lib folder within the project.
I've also added "src/red-folder.com/wwwroot/lib/*" to .gitignore to avoid storing the packages into github.
Fingers crossed, this should have set up the project ready to start copying across my existing code.
I've checked in at this point under this [commit](https://github.com/Red-Folder/red-folder.com/commit/9879d4f6aaaea2ddb04020127c1c481333017d35).

## Good place to stop
That seems like a good place to start for this article.

The next article will consist of a fair amount of copy &amp; paste of existing content - then any refactoring work.
