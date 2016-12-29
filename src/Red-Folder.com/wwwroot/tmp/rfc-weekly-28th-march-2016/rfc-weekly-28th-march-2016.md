## Grunt &amp; Gulp
During the course of looking at converting red-folder.com over to Asp.Net core (see Shameless self promotion below) I've needed to start looking at build tools.

A number of things that just happened under previous version (minification, bundling, less compilation, etc) have now been moved out into separate tooling.  This makes a lot of sense as there is a very rich level of tooling out there - so why not make use of it Visual Studio projects?

In truth, those people that have been working one toe in .Net world and one toe out have been seeing the benefits of non-Microsoft tooling for some time.

Now I've started my journey into these build tools, I can see the power and am likely to utilise more heavily.

Build tools themselves are hardly a new thing.  I was using Make 20+ years ago.  The beauty of the JavaScript build tools are that so much has been done for you - both Gulp &amp; Grunt have very rich plugin libraries.

Ok, so there is a lot of content out there for these two tools - and a lot of comparisons.  Personally I favour Gulp over Grunt (due to it being code over config) - however both can be used to achieve the same result.

Some good resources;

* Pluralsight - [Introduction to Grunt](http://app.pluralsight.com/courses/grunt-introduction)
* [Using Gulp with Visual Studio](http://docs.asp.net/en/latest/client-side/using-gulp.html)
* Pluralsight - [JavaScript Build Automation With Gulp.js](http://app.pluralsight.com/courses/javascript-build-automation-gulpjs)

## MVC 6 Tag Helpers
Again something I've touched on in my Asp.Net Core series.

This MSDN [article](https://blogs.msdn.microsoft.com/cdndevs/2015/08/06/a-complete-guide-to-the-mvc-6-tag-helpers/) goes through the new Tag Helpers.

> "Tag Helpers are a new feature in MVC that you can use for generating HTML. The syntax looks like HTML (elements and attributes) but is processed by Razor on the server. Tag Helpers are in many ways an alternative syntax to Html Helper methods but they also provide some functionality that was either difficult or impossible to do with helper methods." MSDN

## Beyond Page Objects: Next Generation Test Automation with Serenity and the Screenplay Pattern
I've recently been involved with an externally created regression test suite.  It was built from C#, nunit, Selenium &amp; a custom framework.  During my review of it, I found that it wasn't practical on two levels - firstly, it wasn't "non-developer" readable and secondly the code was poorly constructed.  Neither of which boded well for a maintainable test suite.

So I was interested to read this [article](http://www.infoq.com/articles/Beyond-Page-Objects-Test-Automation-Serenity-Screenplay) which looks at the Screenplay pattern provided by Serenity which seemed a much more natural way of composing your tests.

Serenity is a Java (for the above project I'd prefer C#) BDD framework.  For now, my favourite C# BDD remains SpecFlow, but it will be interesting to see if the is a C# port at some point.

## ASP.NET WebHooks RC 1
This [article](http://www.infoq.com/news/2016/03/WebHooks-RC1) takes a first look at the ASP.NET WebHooks functionality.

WebHooks are where you provide a callback endpoint to a service (normally a 3rd party) when you want to get updates.  I've had experience of creating them for use with SendGrid (SaaS email marketing system) - in which I received updated on when customer opened or clicked through on emails.  This was accomplished by providing a simple web service and passing the URL to SendGrid.

This isn't participially difficult stuff, but this seems to be trying to make life a little quicker and easier by having provided all the boilerplate work.

## Will WebSocket survive HTTP/2?
Interesting [article](http://www.infoq.com/articles/websocket-and-http2-coexist) looking at HTTP/2.  Most of the article is looking at if WebSockets has a life after HTTP/2 (spoiler -> yes, it does).

What I found most interesting through was that bundling becomes less important under HTTP/2 due to the way it handles its connections to the server.  This will be interesting to keep an eye on as it would remove one step from the traditional web build pipeline.

## Shameless self promotion
Two articles this week;

[Converting to ASP.Net Core - Part 2](/blog/converting-to-aspnet-core-part-2) - this builds on the basic template I created in part 1 and copies in all of the red-folder.com content (controllers, views, images, scripts, css).  By the end of the article I have the website up and running - minus minification and bundling (which I'll look at more in part 3).  I also start to introduce Gulp in the project.  I had planned to save this for part 3, but I need to compile my less to css.

[The Zeigarnik Effect](/blog/the-zeigarnik-effect).  In the latest of my ROI series, I look at the Zeigarnik Effect, a psychological effect which promotes limiting the tasks we attempt and focusing on completing them.  This helps towards the internal motivators for team individuals and improves productivity.
