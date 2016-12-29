Azure Functions protect against DDOS attack
-------------------------------------------
Great [article](https://www.troyhunt.com/azure-functions-in-practice/) by Troy Hunt (security God) on using Azure Functions to automate firewall rules to block DDOS traffic.

Troy was motivated due to his well known [https://haveibeenpwned.com/](https://haveibeenpwned.com/) being subject to DDOS attacks.  While his solution was handling it (site was still up), it was doing so in an expensive manner by scaling out on Azure.

So Troy was motivated to use clever methods to block out the DDOS attacks.  The article shows how he used Azure Functions to process "problem IPs" and add to his Cloudfront firewall (Cloudfront is providing a CND over his site - free service - I use it).


Implementation Strategies for the Repository Pattern with Entity Framework, Dapper, and Chain
---------------------------------------------------------------------------------------------
This [article](https://www.infoq.com/articles/repository-implementation-strategies) (part 1 of 2) looks at building C# Repository pattern with Entity Framework, Dapper and Chain.

This first article gives a practical code summary of how to perform CRUD opperations using the three ORMs.  It also provides a small benchmarking analysis between the three - although personally, I'd be cautious of using it as a means of choosing an ORM - you reallu need to address with your own usage patterns.

I most cases, the ORM will have minimal impact on peformance.

.netstandard Libraries
----------------------
I've been spending a little time trying to write libraries code for .Net4.5+ and Core.  .netstandard should solve this.

Howver, it really doesn't seem to be as simple as it should be.  From what I can see, this is mainly because the tooling just isn't there yet.

As such, there seems to be a fair amount of "tweaking" to get it right.  In theory you should be able to create a .Core Library targeting netstandard - but this simply doesn't seem to work.  The best options seems to be create a PCL library and get that to target netstandard ... but even that seems to have problems.

I created a library following these steps:

* New Project
* Class Library (Portable)
* Select any targets (you'll change them later) - I go for .Net Framework 4.6 & ASP.Net Core 1.0
* Change the project.json to:

```
{
  "supports": {},
  "dependencies": {
	"NETStandard.Library": "1.6.0",
	"Microsoft.NETCore.Portable.Compatibility": "1.0.1"
  },
  "frameworks": {
	"netstandard1.2": {}
  }
}
```

* If you then look at the Project Properties, you should see the following (see the targeting section):
![Project Properties targetting netstandard](/media/blog/rfc-weekly-24th-October-2016/ProjectTarget.png)

This seemed to work for a .Net4.5 project.  But didn't seem to be working in my Asp.Net Core website project.  The reference could be added, but the namespace couldn't be found and Visual Studio generated compile errors.

Seem to have been finally resolved with install of update KB3165756 (or the subsequenet reboot).  Either way, it seems to be working - although I'm far from confident that there won't be problems further down the line.

Shameless self promotion
------------------------
Release the 3rd part of my ROI of Scrum articles.  It can be found [here](/blog/roi-of-scrum-part-3-benefits).  In this part I look at parts of the Scrum framework and how they can lead to better ROI.
