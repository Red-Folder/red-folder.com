I've written quite a bit previously about migrating to Asp.Net Core - but upgrading to version 2 is long overdue.

And if I'm honest, I didn't plan to do it now.

But life happens.

I've not touched my Red-Folder.com wesbite project for a while ... so when I did, I accidently opened in VS2017 rather than VS2015 and I start down the rabbit hole of project template upgrade.

So I figure, why not.  Let's upgrade to 2 and get it done.

These aren't going to be detailed instructions (as I didn't plan to blog on it) - but, as always, I think there are pieces that will be useful if I ever need to do it again.

## Migration from project.json to .csproj
While it may have divided the community - the actual migration to .csproj was simple enough.

Open in VS2017 and it did the automatic project migration.

The Red-Folder.com project is fairly simple, so there was no problems in that.

## Upgrade from .Net Core 1 to 2
Now this is where it became a little more complicated.

Largely I followed this [article](https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/).

But I found a few additional problems.

Following advice from this [Stack Overflow answer](https://stackoverflow.com/a/45792682), I removed the following from my .csproj files:

```
<RuntimeFrameworkVersion>1.1.2</RuntimeFrameworkVersion>
```

I also found that I need to amend the following in my .csproj files, from:

```
<PackageTargetFallback>$(PackageTargetFallback);dnxcore50</PackageTargetFallback>
```

to 

```
<AssertTargetFallback>$(AssertTargetFallback);dnxcore50</AssertTargetFallback>
```

All in all, not too complicated - just took a little time working through it.

## Visual Studio Team Service (VSTS)
I've the had to update my Continious Delivery project in VSTS.

All Dotnet tasks - I updated the version to 2.* and direct references to the new .csproj file (previously would have been targetting the project.json file).

Simplified the Dotnet Test tasks - previously I passed in a -xml to dotnet test and had a publish task.  Now I can drop the -xml, tick the "Publish test results" and remove the extra publish task.

## Good to go
And that's it.

Probably about 2 hours of work - made longer by picking at the task as I had time over the last couple of days.

But its got it done.  One last thing on the list.