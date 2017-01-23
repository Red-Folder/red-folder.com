## Happy New Year
And welcome back.  I've had a relatively quite couple of weeks ... now back into work.

## Blog migration
While still work to do, the blog is now migrated.  I've moved the entire content from Blogger and setup redirects for SEO.  I've also duplicated the LinkedIn content.

Moving forward I will put all my new posts under [https://www.red-folder.com/blog](https://www.red-folder.com/blog)

There are further technical changes that need to occur - not least is to separate the blog "content" from the red-folder.com website code.  The intention is to put the blog content (in markdown format) into a separate Github repo, then on change (webhook) process the changes (Azure Function), generate HTML content and copy that (and images) to the webserver.  In parallel to that, also kick off tasks (again Azure Functions) to remove relevant pages from the Cloudflare cache.  This was pretty much what I had in mind originally (similar to the Microsoft Docs approach) - but it has been easier to get the basics into place as part of the website code.

There are plenty of additional "blog" functions that I need to site - I'll probably work through them as time allows.

One interesting thing that I would like to do (probably March/ April time) is to use the blog as a demonstration for different JavaScript frameworks.  What I have in mind is to have a version selector on the blog page which changes the presentation logic from ASP.Net MVC Core, to Angular 1.x, to Angular 2.x to React.

Again, like everything about the blog, it’s more a learning/ demonstration tool that anything life changing.

## Contractor Process
As I move towards the end of a contract, I start a number of activities to improve my chances of lining up the next one.  As my current contract is due to end 3rd March, I thought it would be interesting to share my process.

Note; I have ever assurance and expectation that this current contract will be extended.  There is however value for me still going through most of this process to bring everything up to date.  And of course, you can never guarantee a contract until it’s signed (and even then there are questions, but that is a different conversation).

About 4 weeks out, I will update my offline CV, LinkedIn and [website BIO](https://red-folder.com/MyBio).  I’ll need to update on the current contract including responsibilities and achievements and the MCSD: App Builder certification.

About 3 weeks out, I’ll upload my updated CV to a variety of the jobs boards (CWJobs, JobServe, JobSite and Monster).  I will also update any automatic alerts I have on those sites.

About 2 weeks out, I’ll start to reach out to my network (LinkedIn updates, emails, phone calls, etc).

About 1 week out, the wife will start preparing a list of DIY jobs for around the house.  This provides a considerable level of motivation.

In parallel to the above, about week 4, I will also start on a formalising any handover activity.  Dependant on the handover effort required, I will then schedule that into the final weeks.  If done right, I should have any loose ends tied up nicely before I finish.

So as it currently stands, I've about 2 weeks before I would start that.  Hopefully enough time to give the website a bit of spring clean.

## Ethical Hacking progress
I generally find that the Christmas/ New Year period is a great opportunity to get some study in.  I'm not entirely sure the wife agrees that watching courses on Ethical Hacking is entirely family friendly ... but it was better than reading the JavaScript Reference manual cover to cover (I think that was Xmas 2013).

All the hard work/ family ignoring has got me up to 54% on the [Pluralsight Ethical Hacking](https://app.pluralsight.com/paths/certificate/ethical-hacking) Path.
![Ethical Hacking Progress](/media/blog/rfc-weekly-23rd-January-2017/PluralsightEHPath.PNG)

So this has covered a lot of ground since last time I provided an update:

[System Hacking](https://app.pluralsight.com/library/courses/ethical-hacking-system-hacking):
![System Hacking](/media/blog/rfc-weekly-23rd-january-2017/SystemHacking-LearningCheck.PNG)

[Malware Threats](https://app.pluralsight.com/library/courses/ethical-hacking-malware-threats):
![Malware Threats](/media/blog/rfc-weekly-23rd-january-2017/MalwareThreats-LearningCheck.PNG)

[Sniffing](https://app.pluralsight.com/library/courses/ethical-hacking-sniffing) - no Learning Path tests available

[Buffer Overflow](https://app.pluralsight.com/library/courses/ethical-hacking-buffer-overflow) - no Learning Path tests available

[Social Engineering](https://app.pluralsight.com/library/courses/ethical-hacking-social-engineering)
![Social Engineering](/media/blog/rfc-weekly-23rd-january-2017/SocialEngineering-LearningCheck.PNG)

[Denial of Service](https://app.pluralsight.com/library/courses/ethical-hacking-denial-service)
![Denial of Service](/media/blog/rfc-weekly-23rd-january-2017/DenialOfService-LearningCheck.PNG)

[Session Hijacking](https://app.pluralsight.com/library/courses/ethical-hacking-session-hijacking)
![Session Hijacking](/media/blog/rfc-weekly-23rd-january-2017/SessionHijacking-LearningCheck.PNG)

I'd like to complete the full course by end of February.  There is still a lot of content, so that maybe a tall order.

## Cordova & Android Background Service Plugin
So a little history; back in 2012 I wanted to help a friend with problem - they had built an app in Phonegap and wanted to perform some background activity.

I built a plugin, found it was reasonably useful and eventually released it to the world ... this was eventually became [bgs-core](https://github.com/Red-Folder/bgs-core).

For many years I supported this as an open source project and put a huge amount of effort in.

Overtime however it became more and more difficult to manage - combination of the Android development environment and it simply not being a priority to me.  I'm personally not fond of Cordova/ Phonegap - it was there to serve a purpose (fill the gap until phones had better capabilities) - I'd argue that we've reached that point.  I worry that Cordova still has a life because it is being used by web developers to turn their websites into Apps - this really isn't an appropriate idea if the app has any level of functionality.

That being said; I do feel bad that I'm largely ignoring the project.

In an ideal world, I would get back to providing some level of support - I'll have to see if my time allows (neither Cordova or Android are a priority to me).

What maybe more beneficial is looking at alternatives to Cordova - look at better ways of producing the canonical GPS tracker.  I feel that Progressive Web Apps should provide an appropriate alternative.  This is something for me to look into.

## Shameless self-promotion
I'd like to say that I've got fresh ROI content ready to go.

I'd like to ... but it wouldn't be true.

Those posts generally take quite a bit of time to produce - something which has recently been consumed watching way too many TV boxsets on Amazon & Netflix.  But I'm slowly getting myself back on track.  Hopefully something before we get too far into February.

I have however managed to produce some mini-posts on some technical topics I’ve been looking at lately.

[Stateful MicroServices](https://www.red-folder.com/blog/stateful-microservices) takes a look options available on the Azure platform for simple (and cheap) stateful services.  I wanted to take a look at this as part of the blog content processing I mentioned above.

[.Net Core Automated Acceptance Tests](https://www.red-folder.com/blog/dotnetcore-automated-acceptance-tests) takes a look at the readiness of my favourite Acceptance Testing tools SpecFlow & Selenium when used with .Net Core.  In short, they don’t seem ready but the community are coming up with some interesting workarounds.

[Azure Functions Continuous Delivery](https://www.red-folder.com/blog/azure-functions-continuous-delivery) takes a look at building a basic pipeline using Visual Studio Team Services and Octopus to continuously deliver Azure Functions.  This follows the recent preview release of Visual Studio tools for Azure Functions.
