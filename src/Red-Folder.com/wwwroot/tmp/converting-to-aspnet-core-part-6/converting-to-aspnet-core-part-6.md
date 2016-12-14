This is the sixth in my series of converting my red-folder.com site over to ASP.Net Core &amp; MVC 6.

The [first article ](http://red-folder.blogspot.co.uk/2016/03/converting-to-aspnet-core-part-1.html)looked at creating an empty project.

The [second article](http://red-folder.blogspot.co.uk/2016/03/converting-to-aspnet-core-part-2.html) looked at copying my existing content into place and getting it to run.

The [third article](http://red-folder.blogspot.co.uk/2016/04/converting-to-aspnet-core-part-3.html) look at getting it up into Azure.

The [fourth article](http://red-folder.blogspot.co.uk/2016/04/converting-to-aspnet-core-part-4.html) took a deeper dive into the Gulp pipeline.

The much delayed[ fifth article](http://red-folder.blogspot.co.uk/2016/07/converting-to-aspnet-core-part-5.html) added a simple WebApi (and a basic Angular app).

In this article, I'm adding a database behind the WebApi using Entity Framework 7.

## Summary
This article was fairly simple.  While Entity Framework 7 seems to be at early stages, the parts I've used are very similar to EF6.  Even the deployment to Azure went simply enough.
So for this article I converted the hard coded Repository information being provided by the api/repo WebApi to being taken from the database.
At time of writing, the article was up to the following commit -> [https://github.com/Red-Folder/red-folder.com/commit/f27cd74842fa47da04e0f2215508e71503aecca8](https://github.com/Red-Folder/red-folder.com/commit/f27cd74842fa47da04e0f2215508e71503aecca8)

## Changes
As I say above, the work to add a code first repository is very similar to EF6.  Based on the articles I followed, there does seem to be some question over how much will change as EF7 is readied for release - especially the database seeding.<ol>* I've added RepoContext along with DB specific objects.  I wanted to keep separation between the models I used for persistence and the WebApi - so I created new models under the data folder/ namespace.

* I've created a Seeding class which populates the DB will data.  Currently the data is just a copy of the hard coded data I had in the WebApi.  There does seems to be suggestion that EF7 may improve on this seeding in the future.
* Enabled EF7 database migrations - this created the Migrations folder and code.  Again this seems the same as EF6.  So far however I've not needed to update it - but I expect it to be the same.
* Changed the RepoRepository to use the RepoContext (rather than the hard coded values).  It also converts the persistence models to models used by the WebApi
* The Startup has been amended to load configuration settings (see below), create a RepoContext &amp; RepoContextSeedData for dependency injection (see below) and runs the Seeder on Configure

And that was pretty much it.  All I'm currently doing is creating a couple of tables, seeding them and reading from them at run-time.

## Configuration
As with any database connection string, this needed to come from configuration.
The first surprise is that web.config has gone.  You can now effectively load your config from anywhere.  There does seem to be a convention to use a Json file called appsettings.json - but it does seem you could call it anything (I wonder if this may change to become more convention over configuration).

I pass in the appsetting.json to a ConfigurationBuilder in Startup.  I also ask it to check the environment variables for config.  This allows me to specify the development connection string in appsetting.json, but then use Azure portal connection string (which is exposed as an environment variable) to override that with production details when released.

This [Stack Overflow article](http://stackoverflow.com/questions/31097933/setting-the-sql-connection-string-for-asp-net-5-web-app-in-azure) is a goldmine for seeing how to do this.

## Dependency Injection
I think this article more than any other has driven home just how much that you are being pushed towards dependency injection.

It probably hadn't occurred to me because I'm fairly comfortable about it.  But when a recent Podcast called it out as being quite "opinionated" that I realised just how much of a change this might be for an inexperienced programmer.

If you or team aren't comfortable with the principle I'd advise that you do some research before you go too far into Asp.Net Core.  (Don't worry, it isn't that complex - just a better idea to understand it in isolation).

## What next
Ok, I think I've gone as far as I want to with RC1.  My next step is to make the leap to RTM.

I had considered making the step between RC1 to RC2, but the advice I've seen pushes me just go to RTM.  There does sound like a number of last minute changes that the .Net team have received abuse about.  Personally I'd rather they got it right as we'll be living with this technology stack for the next 10 years+.

More once I've had a chance to upgrade.
