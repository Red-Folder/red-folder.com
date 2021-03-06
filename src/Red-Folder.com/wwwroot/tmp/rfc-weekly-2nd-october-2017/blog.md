Ok, so almost a month and half since the last article ... 

Let's pretend its because I've been doing something very very important and haven't had the time.

Ok ... largely it was holiday and being caught up in life ... but let's see if I can get back to it.

Other than a family holiday, the things that I've been focused on:

## Graylog - logging and metrics
I've been putting into place a logging and metrics solutions for a distributed solution using Graylog (similar to the ELK stack).

Part of this have been working with the development team in question to define a best practice for how to get data into Graylog - along with the corresponding infrastructure team to get a reasonably beefy Graylog cluster into place.

The more I work with distributed systems, the more that logging and metrics is becoming a priority to me.

I'd almost go as far as to say that they should be first class citizens in any system being built.  Everything else feeds off that - otherwise how do you know your code is working.

Still work to get that all complete - but certianly interesting stuff.

## TeamCity
The DevOps goodness continues with TeamCity.  I'm currently looking to move build processes from a TFS 2013 (using XAML build defintions) to TeamCity.

It looks like it will be a manual process to migrate the build definitions - but does give the oppertunity to clean the up.

I'll also be looking to move them across into [Kotlin DSL](https://confluence.jetbrains.com/display/TCD10/Kotlin+DSL) - this looks a great way to get the build configuration as code (with all the goodness that comes with that).

For all my love of VSTS - this looks to be one area where TeamCity wins.

## Site Reliability Engineering
And yet more DevOps ....

Working my way through the [Google Site Reliability Engineering book](https://landing.google.com/sre/book.html) - so far this has some great content.

Not only is it looking at the practical side of DevOps, but also the mindset that need to be fostered throughout the buisness.

I'm expecting great things from this book and SRE over the coming years.  SRE feels like the next step for DevOps.

## Docker
And ....

I think I've finally found a pet project to use Docker.

I've wanted to play with it for a while now - but this project would really benefit from the ability to spin services up quickly and cleanly.  Its part of a wider project that I hope to talk about later in the month.  It security based and will allow me to pull in a number of interesting technologies including Docker, ASP.Net Core 2.0 and the [Cypress End-to-End testing tools](https://www.cypress.io/).

## Legacy Code
And ...

I'm expecting to be picking up a major project in the coming weeks working with Legacy Code - so I'm taking the oppertunity to re-read the great [Working effectively with legacy code book](https://www.amazon.co.uk/Working-Effectively-Legacy-Michael-Feathers/dp/0131177052).

On top of that I'm prepare various strategies for handling that code and getting it into a supportable manner.  Hopefully I should be able to blog a bit more about the processes I plan on using in the coming weeks.

## Blog system
The re-write of my blogging system is going to take a backstep again.

I really can't justify working on it at the moment while I have so much over work.

I did manage to get some restructing into the code base with reasonably unit & end-to-end tests inplace.  These have helped me to increase the quality of the code - but there is still plenty I want to do with it before I push to production.

## CV Update
Something that will also take a backstep.  I've made some draft changes and had some great feedback via [LinkedIn](https://www.linkedin.com/feed/update/urn:li:activity:6315638686281277440)

Based on the feedback, I may tone down the Certification/ Exam History sections (combine in some way) to allow me to provide a written profile.  I think a positioning statement on what I'm all about will really add value.

As an aside, I also prompted a debate regarding [Microsoft Certifications on CVs](https://www.linkedin.com/feed/update/urn:li:activity:6316234665212674048).  Some of the responses where interesting (if slightly mising the point of the question).  Again, I'll feed that into the CV.  On balance however I still beleive there is value in the certificates - I've personnally found them useful - both in terms of learning and demonstrating technical capability.
