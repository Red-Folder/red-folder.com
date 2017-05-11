In this article, I look at how to pull in additional projects for use with Azure Functions - in the currently correct way.

Current, because it looks like the Azure Functions (via Visual Studio) is intended to go down the path of a class library.

## The interim step
So this feels like a bit of an interim step.

I've followed the [Publishing a .NET class library as a Function App](https://blogs.msdn.microsoft.com/appserviceteam/2017/03/16/publishing-a-net-class-library-as-a-function-app/) article to convert from the .funproj to a Web Application.

Largly, the blog is easy enough to follow - and took me about an hour.

The biggest and probably most time consuming task was getting the project references and imports correct.  This is just time consuming - not really a problem - although I did find that I needed to consolidate nuget version across projects.

All my unit & integration tests worked fine and I was able to run locally (thanks to the Azure Function Cli - or rather [Azure Functions Core Tools](https://blogs.msdn.microsoft.com/appserviceteam/2017/03/16/publishing-a-net-class-library-as-a-function-app/))

## Continuous Integration
Ok, so then on to getting it to work with my VSTS build.

First step - I remove the pre and post build tasks that I talked about in the [last article](/blog/azure-functions-project-references-part-1)

And to be honest that is about it.

I did update my build ever so slightly to remove unnecessary files (see the article on [Azure Functions Continuous Delivery](/blog/azure-functions-continuous-delivery)).

And it all runs.

Initially the Azure Portal seems a little confused about what it should display (initially I had errors about missing .csx files).  Didn't seem to affect the running of the functions and soon (I'm talking < 15 minutes) seemed to sort itself out.

Next steps will be to build on this version and extend the functionality - more of that in future posts.

## Keeping current
There is no getting away from staying current increasing your work load.

I've felt a fair amount of that with using early versions of Azure Functions and .Net Core ... but it is really worth it to understand the direction of travel of key technologies.

Plus its every so much fun.