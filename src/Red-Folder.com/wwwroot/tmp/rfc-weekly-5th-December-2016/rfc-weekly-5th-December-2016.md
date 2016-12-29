#.Net Core, React & DevOps
Looks like I may have a project coming up which allows me to utilise the .Net Core & DevOps skills I've been developing over the past year or so.

The project is also likely to include React as well.  I'm new to React so it will be interesting to understand and compare to the work I've done so far with Angular.

Expect more content on these areas as I drill further into them.  Exciting times.

## The Science of Great UI
Interesting course referenced in the DotNetRocks Podcast, [the Science of Great UI](http://app.deviq.com/courses/the-science-of-great-ui)

The course claims to go explore "the essence of great design and usability".  Mark Miller, the author and host, has developed a huge amount of research around this thinking so it promises to be quite education course.

The price however seems very high - a 5 hour online course for $99.  As such, at the moment, this isn't one I've forked out for.  It may however be one to come back to as I really can't see them keeping it at that price.

## Azure App Service Plan gotcha
This has caught me out a couple of times now - generally resulting in me taking my www.red-folder.com site offline and then scratching my head to why.

It comes down to understanding what role the App Service Plan plays in the hosting of your Azure App.

So I host my www.red-folder.com on Azure as a Website App.  I also have a number of other websites I host up there from time to time.  Sometimes these will be short lived or be of low importance, so I think to myself "I'll go and downgrade the server ... no point spending more than needed" - so off I go and reduce it down.  In the last case to the Free version.

What I fail to remember is that the Website App lives on top of an App Service Plan.  That App Service Plan controls the type of machine (and thus cost) - and in my case I was sharing that App Service Plan across all of my Website Apps.

Thus when I reduced my test App, I also reduced www.red-folder.com - which in this case broke the custom domain because the Free variant doesn't support it.

The fix (and isolation) is easy enough - create a separate App Plan for the alternative Web App - THEN AMEND IT.

One thing to note.  If you have an existing Website App, to change it to an alternative Service App Plan, it must match the same Region & Resource Group - otherwise it simple won't be available as an option.

If in doubt, I go for removing the Website App and recreating with a separate resource group & Service App Plan.

As an aside; I've now added a subscription to [StatusCake](https://www.statuscake.com/) (free alternative of Pingdom) - this way hopefully when I break it again, at least I can fix it quickly.

(Note, I've also separated off the Service App Plan for www.red-folder.com).

## Ethical Hacking progress
As mentioned last week, I've started on the [Pluralsight Ethical Hacking](https://app.pluralsight.com/paths/certificate/ethical-hacking) Path.

So far, I'm 10% through the path
![Ethical Hacking Progress](/media/blog/rfc-weekly-5th-December-2016/PluralsightEHPath.PNG)

I've completed the first of those courses - [Understanding Ethical Hacking](https://app.pluralsight.com/library/courses/ethical-hacking-understanding).  This course is a bit of a primer to the rest of the course.  It talks about what a hacker is, how an ethical hacker is different, terminology, how hacker are planned and carried out, etc.  It also goes through the steps to setup a lab environment for later courses.  Great introduction so far.

I passed the mini test easy enough ... so I must have been paying attention.
![Understanding Ethical Hacking Learning Check](/media/blog/rfc-weekly-5th-December-2016/LearningCheck.PNG)

Small little snippet that came out of the course - photocopiers have hard disks.  The course talks of an example where a photocopier has been used in a law firm, later sold and the buyer was able to retrieve confidential information still on the disk.  The disk is there to improve the scan rate by buffering before reproducing.  Really isn't a device I would have ever thought about as a leak source.

Every day is a school day.

Next course to look at is [Reconnaissance/ Footprinting](https://app.pluralsight.com/library/courses/ethical-hacking-reconnaissance-footprinting)

##Shameless self-promotion
Last week I published [ROI of Contractor Status](/blog/roi-of-contractor-status).  I've wanted to talk about IR35 and how it is important for Customers to understand this when recruiting a contractor.  I feel it is a really import part of making sure that the engagement is a mutually beneficial relationship.  With the recent changes announced by the government (Autumn 2016 statement), I felt I needed to rush a response out.  Hopefully (fingers crossed) this goes some way to helping Agencies and Customers prepare for what looks to be a considerable change.
