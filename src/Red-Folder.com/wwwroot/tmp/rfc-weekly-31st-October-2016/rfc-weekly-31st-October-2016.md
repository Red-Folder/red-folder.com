TDD, Pairing and the Pivotal way
--------------------------------
Great [InfoQ presentation](https://www.infoq.com/presentations/diversity-responsibility) from Neha Batra of Pivotal Labs regarding teamwork.

What I found interesting where a few concrete examples of how to introduce XP concepts like TDD & Pairing to new teams.  I have to admit that she really opened my eyes to a new way of looking at TDD.

Traditionally I've always been a bit unsure of TDD (come from a background of hack 'n' hope).  However I found her explanation of looking at it from a inputs/ outputs perspective to be very enlightening.

So much so, I will no actively try to use TDD in some of my own project work.  I'll see how it goes.

Continuous Delivery for .Net Core
---------------------------------
This week I've been playing around with Visual Studio Online, Octopus Deploy and Azure to put in place a Continuous Deployment pipeline for my www.red-folder.com site.

It’s fair to say that I've hit a few of those tooling shortcomings that I mentioned in last weeks post.  While .Net Core is usable - the tooling and supporting libraries/ documentation/ examples are a little behind.

That being said, I now have a build process that:

* Triggers on commit to Github
* Compiles the website (including the Gulp tasks)
* Runs unit tests
* Packages for Octopus
* Pushes the package to Octopus
* Creates the Octopus Release
* Deploys the Octopus Release to staging (Azure Web App)

Having worked with Jenkins CI (and a bit of Team City) in the past, the build process within Visual Studio Online is fairly similar.  And given that it comes with 240 free minutes, it seem a much better idea than trying to squeeze Jenkins onto my laptop.

Note; that I've found it useful to use by laptop as a build agent while developing the CD pipeline to avoid consuming all of my free minutes experimenting.  Ok it runs a bit slower, but I can play with much more without eating into the 240 minutes.  I also have the advantage of having all the agent files on my laptop so I can investigate deeper if the build has a problem.

My next task is to convert some acceptance tests I have into .Net Core, plumb them into the CD pipeline and then (assuming passed) promote the Octopus Release to production.

Looks like I may need to make some changes to my acceptance tests - I'm struggling to get Specflow to work with .Net Core - I may just drop it out and use xUnit only until Specflow has a chance to catch up.

Hopefully by next weeks update, my code changes should be automatically deployed.  Be good then to improve the code coverage (arguably the wrong way round ... but I've only got myself to blame if I break it).

IR35 impact of Uber court case
------------------------------
I found the ruling on the Uber court case to be very interesting.  Contractor Calculator wrote an article on its impacts on the contracting community [here](http://www.contractorcalculator.co.uk/uber_outcome_disaster_plc_530710_news.aspx)

Personally I wonder if this is a good think in the long term.

Let's take a step back; HMRC basically have rules for two types of engagement - employee or 3rd party supplier.  What they struggle with is the grey middle ground of services being provided by a individual working out of 3rd party.  Generally the HMRC will want to treat that individual as an employee and any invoiced amounts will go through IR35 and (roughly) be taxes in the same way as PAYE.  The individual however is taking much more risk than an employee and doesn't get the benefits that an employee receives - so they would feel it is unfair to treat that invoice as PAYE - rather it is a company to company service and should be taxed as such.

There is a volume of discussion that can be had on the above ... but what I'm finding fascinating is that the Employer (or client) is being dragged into understand this.

From my own experience I find that few employers understand (or care) if a contractor is within IR35 or not.  With the ruling of the Uber case, that may start to change.  Arguably I could, if with IR35, claim that my employer owns me holiday and benefits in line with a "real" employee.  Employers maybe storing up a real problem by not understand this ruling and its potential impact.

The HMRC appears to be pushing the employer to be an active part of the conversation on IR35 position - they have been talking about moving the liability from the contractor to the employer - which will certainly drive different behaviour.  In the first instance, I would expect the contractor market to be hit hard (either through reduced rates or less job).  Over time I would see employers correctly packaging work up so that it is truly a project performed by a third party - making it attractive to contractors and compliant with IR35 rules.

None of this is helped by the lack of understanding within the recruitment space.  Few agencies seem to understand IR35 - even if their contracts are normally written to handle it.

Shameless self promotion
------------------------
Release the 4th and final part of my ROI of Scrum articles.  It can be found [here](/blog/roi-of-scrum-part-4-pitfalls).  In this part I look at some of pitfalls of Scrum and how to address them.

I'm slowly making www.red-folder.com ready to migrate the Blogger content.  As you can see I've started to add new RFC Weeklies to www.red-folder.com directly rather than going through Blogger.  There are a few changes I'd like to make to basic display, then I'll look at migrating the old content.  Hopefully once I've got the Continuous Deployment into place, I should be able to kick through those changes fairly quickly.  I'd like to have retired Blogger by the end of the year (fingers crossed - I know I've other commitments that will leave little time).
