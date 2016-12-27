## Development
### Entity Framework 7
I'm still working my way through the Pluralsight [Building a Web App with ASP.NET 5, MVC 6, EF7 and AngularJS](https://app.pluralsight.com/library/courses/aspdotnet-5-ef7-bootstrap-angular-web-app/table-of-contents) course.  In the EF7 chapter, Shawn Wildermuth shares some of the upcoming goodness of EF7.

I've never been a great fan of EF6 - predominantly on performance grounds.  Like any technique for making development easier - you are producing a trade off between convenience and performance.  For most of the stuff I wanted to be doing, ADO.Net &amp; Stored Procedures was still the best way to go.

I do however see the benefits in EF.  I have to admit all the new features in EF6 snuck up on me and came a pleasant surprise when I re-certified my MSCD.

EF7 is, according to Shawn, a complete re-write - so hopefully some good stuff.  From his demonstration it certainly seemed very similar to EF6 - he worked with code first and migrations - seeding the DB from code, etc.  I'd expect that to be fairly shallow learning curve.

Interesting EF7 will support not just relational databases (SQL Server, Oracle, etc) but also the NoSQL types - such as Mongo.  This will be interesting to dig into.

I plan on re-writing my [Red Folder Consultancy Website](http://www.red-folder.com/) over the coming weeks based the course (see the Self Promotion section for a link to the first article in a series covering this).  Fingers crossed that I'll get back to my Microservices series as well as part of that.

### RxMarbles
A [visual site](http://rxmarbles.com/) to show the effects of various Rx operators.

### Summary of JavaScript frameworks for 2016
Small (and quick) [article](http://www.clock.co.uk/blog/javascript-frameworks-in-2016) just summarising the position of some of the key JavaScript frameworks for 2016 - both front-end and back-end.

## Development Process

### Nuget team discuss how they use Octopus Deploy
Nice quick [YouTube video](https://www.youtube.com/watch?v=1EaiedH8zXw&amp;app=desktop) discussing how the Nuget team utilise Octopus Deploy with Azure as part of their Continuous Deployment pipeline.  Useful if you've never seen Octopus Deploy

### Azure Continuous Delivery
[Podcast](https://www.dotnetrocks.com/default.aspx?showNum=215) with .Net Rocks Team &amp; Jeffrey Palermo.  They talk about experiences of using Azure for Continuous Delivery.  They also cover some of the differences between Continuous Integration, Deployment &amp; Delivery.

### CSP (Content Security Policy)
CSP is a means of providing additional security for your website (predominantly to assist protecting your customer).

> "Content Security Policy (CSP) is an added layer of security that helps to detect and mitigate certain types of attacks, including Cross Site Scripting (XSS) and data injection attacks. These attacks are used for everything from data theft to site defacement or distribution of malware." [MDN](https://developer.mozilla.org/en-US/docs/Web/Security/CSP/Introducing_Content_Security_Policy)

We (as developers) are obviously not using this enough - and if it can help protect our users then we should be.

It seems that historically, browser support has been patchy.  But according to [CanIUse](http://caniuse.com/#search=csp) it seems to be across most of the major browsers.

More information can be found [here](https://developer.mozilla.org/en-US/docs/Web/Security/CSP).

## Other
### Social Engineering Games
Found [this](http://www.social-engineer.org/how-tos/winning-sectf-def-con-22/) interesting - came across it in a podcast.  A security conference made a capture the flag championship from social engineering.

They defined "flags" (such as finding out what operating system was in use, or get an individual to visit a specific URL) for contestants to collect.  The contestants where then given unaware companies to target - effectively in front of a conference audience using phone &amp; email.

Scary stuff.  Fascinating, but scary.

### Open Command Prompt Here
I often find myself working at the windows command prompt - and the first job is to cd to the same location as the folder I'm looking at.

I've just discovered that on the folder, if you hold shift when you right click, you get an option to "Open Command Prompt Here" - genius.

I'm probably the only person on the planet that didn't know that, but it has made my week.

## Self Promotion
### ROI Series
I've released released the next article in my ROI series - [What is Security?](https://www.linkedin.com/pulse/what-security-mark-taylor)

### Converting to ASP.Net Code
I've started a small series of articles about converting a simple ASP.Net MVC 5 website over to ASP.Net Core &amp; MVC 6.

The first article can be found [here](/blog/converting-to-aspnet-core-part-1)

## And finally

### Have I Been pwned?
Useful little [site](https://haveibeenpwned.com/) to see if your details have been part of some of the high profile security breaches.
