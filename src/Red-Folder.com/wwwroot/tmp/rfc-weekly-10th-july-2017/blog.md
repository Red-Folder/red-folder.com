## Microsoft 70-534 - Architecting Microsoft Azure Solutions update
I've now completed the [Pluralsight Learning Path](https://app.pluralsight.com/paths/certificate/azure-solutions-70-534)

I complete the final two courses this week.  Either of which provide a learning check (which is a bit of shame):

* [Design Web Apps](https://app.pluralsight.com/library/courses/architecting-azure-solutions-70-534-web-apps/table-of-contents)
* [Management, Monitoring, and Business Continuity](https://app.pluralsight.com/library/courses/architecting-azure-solutions-70-534-monitoring-continuity/table-of-contents)

The course certainly feels dated.  The instructor, Orin, reminds you constantly that Azure is moving target.  The exam looks to have been updated last November - and does look to have slightly different content that the course.

I've seen a number of blog posts which recommend the PluralSight course - but I'm not confident that they have used since the exam update.

I've now started the [Microsoft OpenEdx course](https://openedx.microsoft.com/courses/course-v1:Microsoft+DEV205Bx+2017_T2/about).  From what I can see it isn't as closely linked to the exam objectives - but it does seem to be covering the same technical diciplines.  More next week once I'm made decent headway.

## Daily nuggets
I realised last week that the effort I put into my learning could be used to provide value-add to my clients.  So I've started to put out a daily nugget based on some interesting technology.  In the case of my current client, I send this out on the group Skype first thing the morning.

Some of these are items I've shared before in my RFC Weekly.

### Web Assembly
http://webassembly.org/
*Elevator pitch:* JavaScript delivered to the browser as binary rather than text to be interpreted – thus small payload/ quicker to run.

*Opinion:* Is looking like a viable technology in the near future (http://caniuse.com/#feat=wasm).  Expect to initially be used to speed up the customer experience.  Longer term expect to see it open up the browser as an Operating System.  See this talk (https://www.youtube.com/watch?v=MiLAE6HMr10) where C# is transpiled to run in the as web assembly code (very much experimental push at the boundaries)

### .Net Standard
https://blogs.msdn.microsoft.com/dotnet/2016/09/26/introducing-net-standard/
*Elevator pitch:* Abstraction above the .net base library (be it full-fat Framework, .Net Core, Xamarin, etc) which allows library developers to develop for all

*How it works:* Each version of .Net Standard will form a contract of APIs to which the underlying runtime needs to handle. When building a .net library you target a .net standard version rather than a runtime.  
![Diagram](https://msdnshared.blob.core.windows.net/media/2016/09/dotnet-tomorrow.png)

*Option:* Feels like it will reach appropriate maturity with 2.0 (to coincide with .Net Core 2.0) – later this summer I think.  Currently version (1.6) is quite narrow in the APIs (because it is having to align with .Net Core’s capabilities).  You will already start to see Nuget packages targeting .Net Standard though – and this will only ramp up once 2.0 is available.

It makes migration of large projects from full-fat framework to .Net Core (will need to happen one day) a how to eat an elephant problem.  You convert library by library to support .Net Standard (which can be done over weeks/ months/ years) – then switch your runtime when ready.  Feels like it creates a reasonable migration strategy.

### Progressive Web Apps
https://developers.google.com/web/fundamentals/getting-started/codelabs/your-first-pwapp/
*Elevator pitch:* A website that can be installed like an app without having to go through an App Store

*How it works:* Using existing web technology, you’re website will be cached to the device (think mobile mainly) – it will be seen as an icon that can be run like any other app.  For me the key is the Service Worker which operates an out-of-process caching layer for network requests – this means, if done right, you website should continue to operate as you go through a tunnel.

*Opinion:* Being pushed by Google for a while now – not sure if will take to the mainstream and kill off the app stores as some comment.  Personally prefer the PWA approach than the Phonegap/ Cordova approach

## Shameless self-promotion
Last week I released [ROI of Public Scrutiny](/blog/roi-of-public-scrutiny) - following on from a blog article by Troy Hunt, I wanted to question if our decisions where under public scrutiny - would it change our thought process?