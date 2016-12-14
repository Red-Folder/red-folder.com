This is the fifth in my series of converting my red-folder.com site over to ASP.Net Core &amp; MVC 6.

The [first article](http://red-folder.blogspot.co.uk/2016/03/converting-to-aspnet-core-part-1.html) looked at creating an empty project.

The [second article](http://red-folder.blogspot.co.uk/2016/03/converting-to-aspnet-core-part-2.html) looked at copying my existing content into place and getting it to run.

The [third article](http://red-folder.blogspot.co.uk/2016/04/converting-to-aspnet-core-part-3.html) look at getting it up into Azure.

The [fourth article](http://red-folder.blogspot.co.uk/2016/04/converting-to-aspnet-core-part-4.html) took a deeper dive into the Gulp pipeline.

In this article, I've added a simple WebApi.

## Summary
This article has a been quite a while in the making - it has been almost 3 months since my last post of the series.

And if I'm honest, very little of that has been due to WebApi or ASP.Net Core.  Most of the time has been taken up being distracted by Angular (and there was a cruise round the Med as well).

I've been keen to learn Angular for a while now - so I admit to having disappeared down somewhat of a rabbit hole while making a very simple App that sits over a very simple WebApi.

So the sum result of almost three months .... fairly little to be honest.  I've a single Api and a single page with a very much Work-In-Progress Angular App.

## The WebApi
The WebApi was pretty much dead simple.

Main differences to previous WebApi:

* We now inherit from Controller (same as the MVC Controllers)
* You can use Route attribute (I think this may have been in a previous version)
* You can inject services - in this case I have a simple data repository service that I inject in using the [FromService] attribute.  These services are defined in the Startup.ConfigureServices
* Setup the CamelCase resolver for Json output (so that the resulting Json is cased as expected) - again this was defined in the Startup.ConfigureServices

And that was pretty much about it.  From a file perspective, take a look at:

* [Startup.cs](https://github.com/Red-Folder/red-folder.com/blob/master/src/Red-Folder.com/Startup.cs) - Adds the RepoRepository to the services list for dependency injection and sets up the correct casing for Json response
* [RepoRespository.cs](https://github.com/Red-Folder/red-folder.com/blob/master/src/Red-Folder.com/Services/RepoRepository.cs) - Concrete repository pattern to provide data.  Currently just a hard coded stub - in future articles to be wired up to a database backend
* [RepoController.cs](https://github.com/Red-Folder/red-folder.com/blob/master/src/Red-Folder.com/Controllers/Api/RepoController.cs) - The WebApi service itself

This gives the following Api -> [http://www.red-folder.com/api/Repo](http://www.red-folder.com/api/Repo)

Results can be seen here in this Postman screenshot (I've talked previously about Postman - a Chrome Extension which is great for Api testing):

![Image](/media/blog/converting-to-aspnet-core-part-5/2016-07-17-2B--2BRepo-2BApi.PNG)

The Api at this stage, along with everything else, is very basic and is intended to be a placeholder to build on later.

## The App
The "app" is where I've spent most of the time - although as I've said, not a great deal to look at.

Currently that app can be seen on a secret page: [http://www.red-folder.com/Home/Repo](http://www.red-folder.com/Home/Repo)

At this stage it looks something like the below and just allows a fairly simple toggling of the Repositories shown based on tags.  Again something to be built on in the future.

![Image](/media/blog/converting-to-aspnet-core-part-5/2016-07-17-2B--2BRepo-2BPage.PNG)

The code is a bit more interesting and can be found [here](https://github.com/Red-Folder/red-folder.com/tree/master/src/Red-Folder.com/wwwroot/scripts/repoExplorer).  Hopefully I should be able to have time to evolve it into something meaningful - but for now it had given me a chance to get across the majority of the basics of Angular 1.x - certainly enough to pick up an existing projects and to know good practice when I see it.

## What next
In the next article I will look at EF7 and pluming the WebApi into the database.

A quick note on ASP.Net Core versions.  I'm still working on RC1, even though it looks like RTM is now available.  I want to continue with my work on RC1 at least into the next article - which roughly completes the whole RC1 end-to-end application.  Following that I will look to convert to RTM (not sure if there is an RC2 first - but I'll deal with that nearer the time.
