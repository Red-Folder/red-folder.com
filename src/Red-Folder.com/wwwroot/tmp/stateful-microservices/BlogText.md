## TL;DR
In this article I look at some methods of producing very simple low cost Microservices on Azure

## Use case
As part of my blog rebuild, I want to separate the blog content from the blog meta data.

I'd expect, in the fullness of time, for my blog content to be stored as individual HTML files.  This will remove the need to convert from Markdown on the fly and allow for CDN caching (not enough people actually read my blog for that to be a real problem).

So I'd like to separate the meta data and hold that in a queriable MicroService.

For example, I would like to have a dictionary of the available keywords with relevant blog urls.  This would be used to power the keyword filter functionality;
![Blog Filter](/media/blog/stateful-microservices/BlogFilter.PNG)

I'd also need things like the presented Url, dates published/ updated, etc.

I'd like to expose these as REST endpoints so that I can use that meta data from the from the client (in future I'd like to produce Angular/ React versions of the blog client).

At the time of writing, I've currently loading this meta information at runtime from flat file (per blog article) from the webserver.  Technically works, but is hardly an efficient process.

## WebApi
Ok, so I could just add additional WebApi endpoints to the www.red-folder.com ASP.Net Core Website.

That's good from a cost perspective as realistically I'm not going to incur any additional charges for the VM.  Maybe a little extra cost for the storage of the processed data.

I would however like to separate this functionality as I move more of it into a MicroService architecture.  Thus I'd prefer not to use this method.

## Azure Functions
Probably my favourite option at this point.  Light and easy to spin up services.  But are by definition stateless.

So I will need to persist the state between calls.

There are a few options available to me:

* Store to local disk
* Azure storage
* Azure SQL Database
* Azure DocumentDB
* Azure Redis Cache

Let's take each of these in turn:

### Store to local disk
An immediate no-no.  There is no guarantee that the contents of the local disk will be available from call to call.  Given the server-less nature of Azure Functions, I think I need to assume that the data may not be there when I go back to it.

And it certainly wouldn't work if scaling.

That all being said, I have done some experiments writing to local disk and it did work.  I just wouldn't expect it to be reliable.

### Azure storage
Likely to be the most cost effective route for what I'm doing.  If I'm using table storage, given the data will likely be small, then we are looking at next to nothing.
![Blog Storage Costs]( /media/blog/stateful-microservices/BlogStorageCosts.PNG)

Table storage seems the most appropriate option.  The data will be quite small, thus no need for full Blog storage.

### Azure SQL Database
I already have an Azure SQL Database instance, so I could definitely use that for storage.

However I want to move away from everything being tied together in a relational database (one of the real challenges of splitting large code bases and dependencies).  Admittedly I could keep the table structure separate and "impose" manual rules (not difficult if I'm the only developer).

I would have to translate the object data into the database - again not a difficult job and I could use EF Core (as I have previously), but it feels an inappropriate use case for the relational DB when all I'm looking to do is persist a trivial object graph.

Now I could use the [JSON storage functionality of Azure SQL Database]( https://docs.microsoft.com/en-us/azure/sql-database/sql-database-json-features) - I've not played with that yet.  But then, surely I'm just using Azure SQL Server is exactly the same was as I would use Azure Table storage.

For this use case, I think the Azure Table storage is more appropriate.

### Azure DocumentDB
Ok, I like Azure DocumentDB, and it was probably my first though.  The only problem is that it is expensive.

This maybe the way I've configured it, but for a very simply store it is costing me circa £30 per month.  Adding further collections to it will undoubtable increase that beyond a price I want to pay for basic data.

Ultimately I would be using Azure DocumentDB for the same level of functionality I can get out Azure Table storage - and paying for a lot of functionality I wouldn't be using.

### Azure Redis Cache
I like the idea of using this project as an excuse to play with Redis - but again it is simply too much for this simple use case.

It's most basic level is circa £12 per month, so definitely looks like attractive.  But again, I would be paying more for something I can achieve with Azure Table Storage.

## Service Fabric
So Service Fabric provides statefull services out of the box.  It is probably one of the selling points of Service Fabric - why persist the state when it can be help in the Fabric.  (Or at the very least, leave the Fabric to get on with it).

I'd certainly like to have a Service Fabric cluster in place to play with, however from a practical perspective, it would simply cost too much to run for my use case.

At the lowest level, I'd need to commission 3 VMs (note that I have no idea if A0 are powerful enough to run a cluster).  This is obviously the most expensive option.  It is also the most powerful and provides a considerable number of features not available to the other platforms.
![Service Fabric Costs]( /media/blog/stateful-microservices /ServiceFabricCosts.PNG)

Definitely a service that I want to look into more in the future.

## Summary
So, for my current use case, Azure Functions with Azure Table Storage seems the most cost effective.

While, for most businesses, the costs involved are trivial - it is important to consider that they would mount up with the number of MicroServices you are using.  With quantity, the more important cost effective becomes.

At some point, I would expect that Service Fabric would become very cost effective - largely driven by the number of MicroServices you have.  As the numbers grow, the management becomes so much greater.  Service Fabric helps with that.

For most non-trivial cases; I'd probably suggest you look at Service Fabric.
