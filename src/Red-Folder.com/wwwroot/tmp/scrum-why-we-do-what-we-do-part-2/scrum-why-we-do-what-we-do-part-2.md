In this series of articles I am looking to outline why Scrum works for us.  During the series I’ll be looking at the challenges in software development today, firstly in general and then specifically with two “traditional” approaches – waterfall and solo working.  In the final part of the series I demonstrate how Scrum works for us to address those challenges.

I was motivated to produce these articles to educate our wider business.  While we have been operating in a Scrum manner for over two years, acquisition has brought many new faces to the table – many of whom are confused by the way we are working.  It’s also a great opportunity to refresh those that have been working with it for a while.

## Introduction
This article addresses waterfall development and the challenges to be overcome.  The [previous article](http://red-folder.blogspot.co.uk/2014/08/scrum-why-we-do-what-we-do-part-1.html) looked at development in general.  In the [next article](http://red-folder.blogspot.co.uk/2014/08/scrum-why-we-do-what-we-do-part-3-solo.html) we'll be looking at solo working.  In the [final part](http://red-folder.blogspot.co.uk/2014/08/scrum-why-we-do-what-we-do-part-4.html) of the series I demonstrate how Scrum works for us to resolve those challenges.

## What is waterfall?
Many development teams are either still familiar with or working within the waterfall methodology.  The waterfall methodology is fairly synonymous with the production line metaphor.  Each part of the process comes one after the other, with teams further down the production line waiting on the proceeding teams to complete their work,

I’ve seen many projects run in this way – and, until the adoption of agile development methodologies, it was considered by most as the correct way of doing things.

Generally each project would follow a sequence of steps:

* Analysis 
* Development
* Testing
* Implementation
* Maintenance

## Analysis
Within the Analysis phase a lot of exploratory work was done to understand the problem being addresses.  This would generally involve the production of a large complex document.  The idea would be to get the work “set in stone” before we invested development time in the project.  With a reasonably large project this process could take weeks, months or even years.  The intention of the document was for it to become the “font of all wisdom”.  All the later parts of the process should be able to reference the document for any question they would have.

Invariably, the analysis section would have problems.  Firstly, if the project was any size then often there would be pressure to get on with it.  Analysis was often seen as a distraction from actually building the system and I’ve seen more than one project team pressurised into starting development with either an incomplete or non-existent analysis process.  This of course meant that the teams later on down the line would have no plan to work from – thus the project could and would easily head in an unexpected direction.  This gives us our first challenge if this article, **How do we allow development to start without documentation?**

Secondly, at the other end of the spectrum, so much time and effort was put into the analysis that the project would effectively stall with its lack of momentum.  Not only would this make it difficult to retain resource for the later stages, it often mean that key characteristics could change over time.  So much so that the original purpose of the project may no longer be valid.  This gives us our next challenges, **How do we maintain momentum?** and **How do we handle change?**

Thirdly, the document, due to its complexity was rarely understood by the entire team.  Often each team in the process would write their own version of the same document.  Developers would produce design documents and technical specs.  Testers would document test plans … and so on.  Each would use their document as gospel – which of course would lead to differences in interpretation and thus delivery.  So **How do we ensure the work is understood by the entire team?**

## Development
It would be common for developers to be removed from many of the original conversation that occurred in analysis.  Thus their primary source to work from would either be the documentation or the project equivalent of Chinese whispers.

This was a fairly demoralising time for the developer.  They were consigned to the backroom, away from the business.  The only interaction generally coming from dry documents from which to produce their work and receiving only negative feedback in the form of bugs.

The process tried to turn developers into machines rather than take full advantage of their capabilities.  It was seen very much as production line work.  A developer (or team) would be booked for a period of time, they would then be expected to produce something within that time and then be ready to move onto the next allocation.  And more often than not, the developer was told how much time they had and what they had to produce through micro management.

I’ll put my hands up to this one.  I’ve management development teams in the past where I have micromanaged them.  At the time, I just didn’t realise how counterproductive that is.  It comes from the fallacy that, I as a manager, I know better than my staff.

The traditional “command and control” management structure I discussed in the last article led us down that path.  The key to unlocking the talent in the developer was engaging the intrinsic motivators for which we already have a challenge.

## Testing
I remember when testing (or Quality Assurance) became the new vogue.  An individual (or team) where responsible for ensuring that the software produced was of significant quality to be used.  The focus on quality was a superb revelation to the development industry.  The simple understanding that finding a bug before it went live was considerably more cost effective was a ray of light.

I certainly saw the testing team as being a great boon to the overall process.  We certainly don’t want to lose this focus on quality in any development process we use – which we have already captured with our **“How do we ensure quality?”** challenge.

It was however rarely given enough priority or focus in the process or the organisation.  It was generally understaffed, under-tooled and not included early enough.

Testing also suffer from being removed from the intended user and, in some cases, even the developer.  Thus they may not be testing the correct thing.

It was generally common that a silo mentality lead to distrust and animosity between the developers and the testers.  Not surprising when you consider that, from a developers perspective, the role of a tester is provide negative feedback in the form of bugs.  I’m sure this led to a number of developers feeling that the testers where out to get them.  This prompts the challenge, **How do we ensure we are working together?**

## Implementation and Maintenance
To keep the development team developing, the work of implementing and maintaining the system would be handed off to a separate support function.

They would be expected to install, configure and run the system again from documentation.  In the event of a problem they would likely need to go back to the original development team – who may have already been disbanded by this point or at the very least be focused on a new project.

Dependant on the development teams focus this could easily result in quite a backlog of production bugs.  In one organisation I inherited, bugs had been considered unimportant compared to new development.  This had left a legacy of over 300 un-investigated tickets and very high customer dissatisfaction.

This is already partly covered by our **“How do we ensure quality?”** challenge, but I’d also like to add **How do we ensure ongoing quality?**

## Project Management
Project managers had a hard time of it trying to keep everything on track.  They had to answer on behalf of teams that would struggle (or refuse) to commit and engage.

Considerable effort would need to be put in by themselves to run meetings, produce plans, gain agreements, handle disputes and generally try to keep everything on track.  This effort quickly escalated as they needed to engage with the various teams to pull this information together.  This prompts **How do we minimize project overhead?**  This it closely tied to **“How do we ensure we are developing the right things?”** because we should be spending time on delivering rather than work that doesn’t produce an ROI.

The project manager themselves would be under pressure to keep to timescales that had been agreed with board meetings to avoid someone up the tree losing face.

“Scope” was one of the favourite words any time a project looked to be in jeopardy of running late or over budget.  I’ve seen many projects having their “scope reduced” so that a project can be presented as being on track.  Often these reductions would come either in the form of technical debt or missing functionality.

Technical debt, like financial debt, is where a project either intentionally or accidentally does something that will incur extra effort further down the line.  This could take the form of more bugs or whole sections of the system that are thrown together with the intention of coming back later and doing it properly.  Even with the best of intentions this debt is unlikely to be serviced and as such grows and grows to the point that the system cannot be maintained or developed.  Thus, **How do we highlight and avoid technical debt?**

Missing functionality is generally more obvious in that something is considered low priority and is simply dropped.  This, like the technical debt, could be the right decision at the time.  It can generally however be very difficult to get a second project started to add this feature at a later date as the business benefits are considerably less.  This is already covered in the two challenges **“How do we ensure that we are developing the right thing?”** and **“How do we ensure that we are developing it at the right time?”**.

## Feedback
One final component of the waterfall process was the feedback mechanism.  Often the originating owner of the project would only see the finished article at the end.  It was only after all the work had been invested would they be able to tell the team if they had got it right or wrong.  And if this occurs months or even years after project initiation then lost investment can be immense.

While there has been project methodologies to address this by providing feedback during the project progression, they have generally suffered from the same problem – translation from techy to business.  The IT teams will want to converse using their language, their documents and their terminology.  The business generally doesn’t understand these so either will accept them without questioning to avoid looking stupid (Emperor’s new clothes) or push for abstracted summaries which lose the deeper meaning.

It is common for a project to be summarised up using RAG status (Red, Amber, Green).  This is great for a quick overview, but if it’s the team building the system that produces the status rather than the intended user, then we are making huge assumptions that we are building what the users wants.

This prompts us to ask **How do we expose something is wrong quickly?**

### Summary
So far we have found the following challenges:

[First article](http://red-folder.blogspot.co.uk/2014/08/scrum-why-we-do-what-we-do-part-1.html):

1) How do we engage the intrinsic motivators?
2) How do we ensure that we are developing the right thing?
3) How do we ensure quality?
4) How do we ensure that we are developing it at the right time?
5) How do we ensure that we are developing only what’s needed?

This article:

6) How do we allow development to start without documentation?
7) How do we maintain momentum?
8) How do we handle change?
9) How do we ensure the work is understood by the entire team?
10) How do we ensure we are working together?
11) How do we ensure ongoing quality?
12) How do we minimize project overhead?
13) How do we highlight and avoid technical debt?
14) How do we expose something is wrong quickly?

In the [next article](http://red-folder.blogspot.co.uk/2014/08/scrum-why-we-do-what-we-do-part-3-solo.html) we will look at solo development.
