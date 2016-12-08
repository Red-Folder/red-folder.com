This is the seventh (and last) in my series of converting my red-folder.com site over to ASP.Net Core &amp; MVC 6.

The [first article](http://red-folder.blogspot.co.uk/2016/03/converting-to-aspnet-core-part-1.html) looked at creating an empty project.

The [second article](http://red-folder.blogspot.co.uk/2016/03/converting-to-aspnet-core-part-2.html) looked at copying my existing content into place and getting it to run.

The [third article](http://red-folder.blogspot.co.uk/2016/04/converting-to-aspnet-core-part-3.html) look at getting it up into Azure.

The [fourth article](http://red-folder.blogspot.co.uk/2016/04/converting-to-aspnet-core-part-4.html) took a deeper dive into the Gulp pipeline.

The much delayed [fifth article ](http://red-folder.blogspot.co.uk/2016/07/converting-to-aspnet-core-part-5.html)added a simple WebApi (and a basic Angular app).

The [sixth article](http://red-folder.blogspot.co.uk/2016/07/converting-to-aspnet-core-part-6.html) adding a database behind the WebApi using Entity Framework 7.

In this, the last of my articles of converting to Asp.Net Core, I’ll look at upgrading from RC1 to RTM.

### Change of direction
From what I’ve read there appears to have been quite shift in thinking between RC1, RC2 and RTM – decisions were made that changes the direction quite a bit during that period.  This upset many in the community.  The final decisions however seem to have been the correct ones – and while there may have been some pain during the transition, it is worth it to get the framework to the right place.

If it is anything like previous incarnations of .Net – then we will live with the decisions made for over the next ten years.  Time will ultimately tell if the correct decision was made.

### In hindsight
I’m adding this section after having converted it – so, in hindsight …

I probably should have started with a new empty project and copied all the content in – it would have been cleaner.

And I should have used the [I Can Has .Net Core site](https://icanhasdot.net/).  Really great tool for highlighting dependencies that you need to change.

Ok … back to the stream of consciousness …

### Upgrade Visual Studio 2015 to Update 3
Possibly the most time consuming part of the whole process.

The Update can be found [here](https://www.visualstudio.com/en-us/news/releasenotes/vs2015-update3-vs).

### Install .Net Core 1.0.0 – VS 2015 Tooling
I had originally thought this was done by Update 3 – but it didn’t seem to be case so I added separately.

I seemed to suffer from an installer bug which claimed that Update 3 hadn’t been applied – telling me to run a fix.

Following the instructions [here](https://docs.microsoft.com/en-us/dotnet/articles/core/windows-prerequisites#issues) I was able to install the tools.

I would expect given time that the tools will be rolled into the Visual Studio updates and it will be relatively automatic.  At this stage, the extra step seems a little tedious.

### Upgrade NuGet
Again, not sure if this is a step that is needed, but seems to be advise to upgrade to 3.5.

I already had 3.5.0.1484 installed, so nothing to do there.

### Migrate from DNX to .Net Core CLI
Most of this was based on this [article](https://docs.microsoft.com/en-us/dotnet/articles/core/migrating-from-dnx)

Where I have previously talked about DNVM (Dot New Version Manager) and DNX (Dotnet Execution Runtime), these have now been replaced by the .Net Core CLI (Command Line Interface).

I’ve had to make the following changes:


* Update the Globals.json sdk version.  Following the above article, I decided to remove the version node so that Visual Studio assumed latest
* I’ve then used “dotnet new –t web” to create a new project.json.  From that I’ve copied most of the setup into my project
* I’ve then run “dotnet restore”



### Reference changes
This [document](https://docs.asp.net/en/latest/migration/rc1-to-rtm.html) provides details of references that have changed ().


* Added a Program.cs
* Make updates to the new libraries (see the Can I has .Net Core site I mentioned above – it’s easier than going through the document)



### Json Serialiser
Json.Net is now defaulting to CamelCase so I could remove the serialiser.  The JsonOutputFormatter setup (so the Json response was CamelCase) need to be changed as the this [article](http://maciejskuratowski.com/2016/03/07/asp-net-core-1-0-camel-case-json/)

### Connection Strings
Change the ConnectionString format within appsettings.json

This caught me out for a while.  Basically I moved it outside of the data node, creating a ConnectionStrings node for the RepoContext.

### Entity Framework Migrations
I found that Migrations was struggling to work – even with all references updated.

In the end I code to completely recreate the migrations with solved the problem.

### FromServices attribute
The [FromServices] DI attribute has changed from being usable with properties to being used only with Action parameters.

### Base Path for the Configuration
Updated the Startup constructor to set the base path.

### Web.Config
Replaced the httpPlatformHandler (and it’s config) with apsNetCore handler.

### PreBuild -> PreCompile
I run gulp tasks on PreBuild (setup in project.json) – or at least I had done.

Took a lot of digging to find that PreBuild had become PreCompile and the corresponding variables had changed as well.  Simple enough once you know how, but it took a lot of digging and blind luck that I found it mentioned in a Stackoverflow response.

### Azure
For reasons that completely escape me, I couldn’t get the project to work on the existing Azure webapp.  Creating a new one, published, all good.  Very strange.

It kept complaining about permissions when I browsed it (felt like it was trying to serve files directly and telling me that I didn’t have permissions to browser).  I suspect the AspNetCore handler wasn’t picking up the requests – but for the life of me I couldn’t see why.

The new webapp solved that problem.

### And everything else I forgot
I suspect I’ve missed a few changes in the above (or have in the wrong order).

I’ve been at it for odd stolen minutes over the past couple of weeks.  All in all it probably would have taken maybe an hour or two end to end.

In the end, everything has been committed under this [commit](https://github.com/Red-Folder/red-folder.com/commit/ca3633e07b0538e03ce76298943c2b6fae9091d6).

### The future
I wanted to have an end to this series.  The move to RTM seems the appropriate place to do so.

I suspect that I will provide a updates in the future.  I suspect that as I dig deeper into the Asp.Net Core, there will be a number of face palm moments.

But I think I’ll do that in new posts as I get to them (for the next few weeks I’ll be updating the site with content aimed at getting me the next contract role).

### Additional links
A collection of additional links that I used during the process:


* [https://wildermuth.com/2016/05/17/Converting-an-ASP-NET-Core-RC1-Project-to-RC2](https://wildermuth.com/2016/05/17/Converting-an-ASP-NET-Core-RC1-Project-to-RC2)
* [https://blog.3d-logic.com/2016/06/08/running-asp-net-core-applications-with-iis-and-antares/](https://blog.3d-logic.com/2016/06/08/running-asp-net-core-applications-with-iis-and-antares/)
* [https://docs.asp.net/en/latest/tutorials/publish-to-azure-webapp-using-vs.html](https://docs.asp.net/en/latest/tutorials/publish-to-azure-webapp-using-vs.html)



 
