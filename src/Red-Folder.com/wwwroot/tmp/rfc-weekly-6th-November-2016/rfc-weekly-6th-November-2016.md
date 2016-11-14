Agile in the Enterprise
-----------------------
A few weeks back I talked about some of the pitfalls to watch for in Scrum adoption - you can see the article [here](https://www.linkedin.com/pulse/roi-scrum-part-4-pitfalls-mark-taylor)

This [article](https://www.infoq.com/articles/agile-enterprise-misconceptions) covers similar territory in regards to going into agile with your eyes open.  The article reads a little like a justification for a requirements gathering tool (which isn't surprising given that the author works for a supplier of such a tool).  I do think however it does raise some questions to think about during agile adoption.  I personally wouldn't run off and get a system to manage requirements, but it is probably something to be asking at periodic phases - would it benefit you?

Internal Tech Conferences
-------------------------
This [article](https://www.infoq.com/articles/internal-tech-conferences) discusses some of the benefits of running an internal tech conference along with some concrete advice.

Running internal tech meet-ups - be they a department brown bag session or a companywide conference can only be a good thing.  An opportunity to share knowledge and get to know each other better is always going to appeal to me.

I really like the idea, mentioned in the article, of using it to explain to the wider (not technical) business what all the techies are up to.

10% time
--------
I've long been a fan of carving out time for staff to focus on non-BAU.  In prior roles I've managed to implement a 10% innovation time which produced interesting results.

This [article](https://www.infoq.com/news/2016/10/learning-autonomy-time) touches on talk given regarding the observed benefits of 10% time.  The slides for the referenced presentation can be found [here](http://www.slideshare.net/giusdesimone/managing-in-the-century-of-networked-society-66173512)

Probably not a surprise that some of the content looks familiar to an amount of my ROI articles.  Nice to see Drive by Daniel Pink being referenced.

I'd certainly advocate any organisation looking at 10% - even if only to allow the team to scratch those itches that they simply don't have the time to get to.  Combine them with something like the Internal Tech Conferences should really be an empowering experience.

Shameless self-promotion
------------------------
I publish ROI of legacy software [here](https://www.linkedin.com/pulse/roi-legacy-software-mark-taylor).  In the continuation of my Return On Investment from Software Development I look at legacy software and challenge the prevailing re-write philosophy.  As a default, I will always favour evolution over revolution - and this this article I provide some of the ROI behind that thinking.

I've continued to work on getting my www.red-folder.com into a Continuous Deployment pipeline.  Assuming all goes well, then this article should have gone through that pipeline (longer term, the blogs need to be separated from the codebase ... but that's for another month).

I do feel somewhat like a bad workman blaming his (preview) tools ... but I have lost a significant amount of time getting caught up in Visual Studio problems.  It struggles with .Net Core & .NetStandard work.

Important lesson; if in doubt, drop out to the command line and use some dotnet restore/ build/ test magic.  I’m commonly finding that errors & red underlining in Visual Studio, will compile and run fine from the dotnet CLI.

Conversely, I'm somewhat in love with Visual Studio Team Services at the moment.  While some tasks haven't been overly obvious (thanks in part to the newness of .Net Core) I think I'm getting there.  Still some work to do with code coverage and some final tweaks to push from Octopus Deploy into Production (as part of the VSTS build process).  Hopefully I'll get into a good place in the near future to outline the steps I'm using.
