### Microsoft Releases TypeScript 2.0 RC
Its it either the season for Release Candidates or it is not just a constant steam (the latter being likely to be the case).

I'd expect a number of further RC's before RTM - but will be interesting to see if this working with Angular 2 or not.

Both are on my long list of technologies to dig into


### Bugs and Documentation Errors in .NET's HttpClient Frustrate Developers
This [article](https://www.infoq.com/news/2016/09/HttpClient) raises a major problem in one of the heavily used .Net object - HttpClient.

As a community we have been using it consistently a certain way (as IDisposable). Turns out this isn't the correct way of running it.

The article really does raise some annoyance over advice that we've been working with for years - later to find that it wasn't the way we should be doing it.

One to take real note of.


### .Net Standards library
This [article](https://docs.microsoft.com/en-us/dotnet/articles/standard/library) introduces the .Net Standards for libraries. For years there has been PCL which allows for library re-use across the Microsoft ecosystem (desktop, server, phone, etc). Generally however most developers will have not needed (or cared) to know about it.

With the growth of multi-operating system being available through .Net Core, then the PCL standards needed to be changed.

The .Net Standards try to provide a clear compatibility for libraries cross environment.

This is likely to be an important concept for .Net developers to get their heads round in the time to come (especially as we transition between .Net and .Net Core)


### Shameless self promotion
No new articles this week.

Work has however been going well on my Azure Functions project that I talked about last week.  I've got the core web crawl function into place - and that is working well.  Its integrated into my post-deployment acceptance tests.

The next action is to visualise crawl - probably through d3.js