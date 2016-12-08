<a href="https://3.bp.blogspot.com/-I_oaP90FoII/VwLSGdVpg-I/AAAAAAAACVI/k8qLuxwepagPz6AVDvfPbqLj0uXtgr0Iw/s1600/RFCWeeklyLogo.png" imageanchor="1">
<img border="0" src="https://3.bp.blogspot.com/-I_oaP90FoII/VwLSGdVpg-I/AAAAAAAACVI/k8qLuxwepagPz6AVDvfPbqLj0uXtgr0Iw/s1600/RFCWeeklyLogo.png" />
</a>
This is the next of my RFC Weeklies - a summary of things that I find interesting.  It is an indulgence; its the weekly update that I would like to receive.  Unfortunately no-one else is producing it so I figured I best get on with it.  Hopefully someone else also find useful.
## Using Gulp to build and deploy .Net apps
Great little [article ](http://www.mikeobrien.net/blog/using-gulp-to-build-and-deploy-dotnet-apps-on-windows/) talking through how to use Gulp for common .Net project tasks.

## Microsoft Graph
Microsoft are aiming to make available all of their online services via [API](https://graph.microsoft.io/en-us/)

Initially this is focused on the Office365 product, but ultimately I believe it is destined to provide an API endpoint for everything that his hosted online.

If your organisation has adopted Office365, this is well worth a look as there will likely be considerable opportunities for integration and automation.

## MSDeploy
Great [podcast](https://www.dotnetrocks.com/?show=1275) talking about the benefits of MSDeploy.

The guest speaker (Robert Schiefer) talks through various scenarios why he and his organisation are using MSDeploy where others would have turned their attention to other tools (Octopus Deploy for example).

Roberts main gripe is that people just aren’t aware of how powerful (and extremely useful) that MSDeploy is – and that they are turning to other tools out of ignorance.

## Azure Functions
[Azure Functions](https://azure.microsoft.com/en-us/blog/introducing-azure-functions/) are a new offering in Azure (in Preview).  They take the *-As-A-Service to the next degree by operating on an event driven on-demand facility.

Rather than provisioning any form of machine, you upload code, step the triggers (and I assume some additional config) and away it goes.  Azure just takes care of it for you.

And because you are only operating on demand then you don’t need to have as much infrastructure commissioned (and thus should be cheaper).  Azure are describing it as the “Serverless” execution model.

## Azure Service Fabric
[Azure Service Fabric](https://azure.microsoft.com/en-us/blog/azure-service-fabric-is-ga/) is now in General Availability.

Microsoft created Service Fabric to support to evolution from monolithic applications to microservice based architecture.

Microsoft has used the Service Fabric for a number of their own services for some time – so it should be battle tested.

Service Fabric provides for both stateful and stateless Microservices.  This is interesting as most frameworks are expecting the microservice to store state in another system (database for example).  

I've actually have a couple of use cases in current projects where stateful microservices would make a really good architectural choice.

## PostCSS
PostCSS is another JavaScript tool.

It is used to transform CSS.  It works by using modules to apply changes to CSS.  The most obvious are functions like validation and vendor specific updates.

It has a library of something like 250 plugins (god knows what they all do).  Have a look through the Plugins library.

Nice idea is the rtlcss plugin which converts your left to right reading website to right to left.

PostCSS can be grabbed by NPM.  The project webpage is [here](http://postcss.org/)

## Shameless self-promotion
Only one article this week;

Within my ROI series on LinkedIn, I’ve taken a look at Developer Anarchy.  This article can be found [here](https://www.linkedin.com/pulse/developer-anarchy-mark-taylor).

The fourth article looking into Asp.Net Core has been taking a bit more time than normal.  Taking a good look at Gulp and how to use to improve the build pipeline.  Trust me – good things come to those that wait.