### [Rollup.js](http://rollupjs.org/)
I’m tempted to describe this as “another” JavaScript bundler tool – similar to Webpack or what can be achieved through Gulp/ Grunt.

Yet one different that jumps out is tree-shaking (maybe present in the others and I’ve just not noticed).  Tree-shaking will walk through the code and ignore code that isn’t used from the resulting output.  This looks to be at JavaScript module level so it isn’t a complete static analysis of your code – but maybe a step in the right direction to ignore unnecessary code.

### [PluralSight Learning Paths](https://www.pluralsight.com/product/paths)
I’ve written before that I’m a fan of PluralSight.

A recent feature I’m enjoying is there Learning Paths.  They have previously defined a number of courses as a path through blog entry.  Now they have built it up to be something a bit more.

The Paths feature collates a number of courses and puts them into skill level (beginner, intermediate, advanced) and then provides tools for measuring your current level.

I’m currently working through the Angular path.  I’m probably about half way through the material (have some experience with Angular previously) and have a achieved a reasonable score.

![Image](/media/blog/rfc-weekly-1st-august-2016AngularScore.PNG)

There is an argument to question the assessment quality – but anything that motivates learning is a good thing by me.

### [Azure SQL Server Stretch Database](https://azure.microsoft.com/en-us/updates/general-availability-sql-server-stretch-database/)
For me Stretch Database is one of that great innovations (assuming it works – not tried it).

The idea is that you extend your internal SQL Server into Azure and threat them as two different speed storage.  You effectively shard your database so that all the speed critical data is stored local and less speed critical in Azure.

This should have significant performance impact for any database that needs speed for “current” work, but also needs to keep historic information.

This sounds to be similar in setup to sharding.

### [Always Encrypted for Azure SQL Database](https://azure.microsoft.com/en-us/updates/general-availability-always-encrypted-for-azure-sql-database/)
Always Encrypted helps ensure that sensitive data is encrypted and decrypted inside client applications or application servers by using keys that are never revealed to Azure SQL Database. As a result, even database administrators, other high-privilege users, or attackers gaining illegal access to Azure SQL Database can't access the data.

### [Simplified MVC Controllers with Medaitr project](https://jonhilton.net/2016/06/06/simplify-your-controllers-with-the-command-pattern-and-mediatr/)
Nice little article introducing the Medaitr project which helps provide separation of concern between the Asp.Net MVC Controller and backend through the use of the command pattern.

### [Cake](http://cakebuild.net/)
Cake (C# Make) is a cross platform build automation system with a C# DSL to do things like compiling code, copy files/folders, running unit tests, compress files and build NuGet packages.
The big advance of Cake as a build tool is that it is using C# - so using the same tools that you’ve developed the code in.

### [DevDocs](http://devdocs.io/)
A site with a collection of free Developer Documentation.

Not sure if it adds much value beyond normal documentation sources – but looked interesting enough to make a note of.

### Shameless self-promotion
Errr ... there isn't any this week.  I've been working on something to promote my consultancy business - but it's not quite baked yet.