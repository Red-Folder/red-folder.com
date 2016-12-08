![Image](/media/blog/rfc-weekly-7th-march-2016RFC%2BWeekly%2BHeader.png)
This is the first of my RFC Weeklies - a summary of things that I find interesting.  It is an indulgence; its the weekly update that I would like to receive.  Unfortunately no-one else is producing it so I figured I best get on with it.  Hopefully someone else also find useful.

## <span style="font-size: x-large;">Development</span>
<h4><u><span style="font-size: large;">ASP.Net Core</span></u></h4>If you've not heard, ASP.Net 5 has been renamed to ASP.Net Core 1.
This is largely to signify the change of direction.  ASP.Net Core is clean start, removing a lot of the ASP.Net legacy.  It will be interesting too see how the final product feels from current ASP.Net and the migration path.
So what is different;
A lot of micro-libraries.  Following on from .Net, there is a move to smaller libraries rather than one massive catch all monolith.  It should make for cleaner, smaller implementations.

There is a lot of Open Source goodness.  The Visual Studio team have borrowed a lot from the cool kids.  We have Node under the hood for tooling, we have Bower for JavaScript package management and further movement toward OWIN middleware.
It looks as if Web Forms have been dropped completely (although I've seen rumours of it being reintroduced later down the line).  Personally, I'm happy to see the move away from Web Forms.  And removal of legacy overhead is probably a big part of how ASP.Net Core is a clean start.
Performance, if nothing else, appears to have been a benefit from this clean start.  I've seen a few tweets quoting significant performance - so it will interesting to see if we see this in the wild.
I'd expect to highlight a number of resources over the coming weeks.

So far I've watched. [Play by Play: ASP.NET Core 1.0 on any OS with John Papa and Shayne Boyer](https://www.pluralsight.com/courses/play-by-play-asp-net-core-1-0-on-any-os-john-papa-shayne-boyer) in which the guys talk about some of the multi OS capabilities of ASP.Net Core and its tools.  Interesting to see that we would be running ASP.Net MVC or WebAPI services from within Docker.  There are also tools for building .Net using a Mac - just where will the madness end?

I'm part way through [Building a Web App with ASP.NET 5, MVC 6, EF7 and AngularJS](https://www.pluralsight.com/courses/aspdotnet-5-ef7-bootstrap-angular-web-app) - which is a course covering the basics of web development will all the new tools.  A fair amount of the content is designed at the beginner, but also shows some the interesting new features within the suite of products.  More on this as I work through it.<h3><u><span style="font-size: large;">
</span></u></h3>### <u><span style="font-size: large;">Breakpoint Generator</span></u>
A new tool from the Visual Studio team.  This new extension is intended to help get to grips with an unfamiliar code based quickly.
It generated breakpoints and adds tracking code into existing code base - hopefully making it easier to get a feel for how the code fits together.
I'm well used to dealing with legacy code - in which I will generally look for the "seams" and isolate functionality into understandable chunks.  This extension seems to be along the same vain.  I'll be interested in trying the extension out in the near future.
Read more about it [here.](https://blogs.msdn.microsoft.com/visualstudioalm/2015/11/19/breakpoint-generator-extension/)
<h3><u><span style="font-size: large;">
</span></u></h3>### <u><span style="font-size: large;">Microsoft Acquire Xamarin</span></u>
Xamarin provides C# developers an ability to produce cross-platform mobile apps.

Personally I've never used, but it has been making a lot of noise in the Microsoft space.  Its main problem to date has been the cost of it.  This has certainly been a barrier.

Hopefully with the Microsoft acquisition, the Xamarin product set will be folded into the Visual Studio tool without extra charge.

More on the article from InfoQ [here.](http://www.infoq.com/news/2016/02/microsoft-xamarin)
## <span style="font-size: x-large;">Career</span>
### <span style="font-size: large;"><u>Microsoft Certification retirement</u></span>
A number of certifications are due for retirement, including:
* 481: Essentials of Developing Windows Store Apps Using HTML5 and JavaScript
* 482: Advanced Windows Store App Development Using HTML5 and JavaScript
* 484: Essentials of Developing Windows Store Apps Using C#
* 485: Advanced Windows Store App Development Using C#
* 488: Developing SharePoint Server 2013 Core Solutions
* 489: Developing SharePoint Server 2013 Advanced Solutions
* 490: Recertification for MCSD: Windows® Store Apps using HTML5
* 491: Recertification for MCSD: Windows Store Apps using C#
* 492: Upgrade Your MCPD: Web Developer 4 to MCSD: Web Applications Examination
* 499: Recertification for MCSD: Application Lifecycle Management
* 517: Recertification for MCSD: SharePoint Applications

The article [here](https://borntolearn.mslearn.net/b/weblog/archive/2016/02/18/additional-details-on-upcoming-exam-retirements) talks about the rational behind the changes.  Interesting to see that the direction away from SharePoint specifically and a specialisation towards Office365 apps.
As the Office365 specialisation builds on top of the MCSD: Web Application, it may be something I explore in the future.## <span style="font-size: x-large;">Contracting News</span>
### <u><span style="font-size: large;">Salary Vs. Dividend 2016/ 2017</span></u>
Most of my recent work has been interim management; as such firmly within IR35, thus dividends aren't a payment method I've used for a while.
It is however likely to be a method I will use again in the future, so always good to keep an eye on any changes.
The Chancellor has introduced some new changes to dividend payments to take effect in the new tax year - effectively losing a notional tax credit.
Contractor Weekly have looked at the differences that the change makes.  In summary, this does reduce the benefit of the dividend - but it is still preferable to salary.
Read the full article [here.](http://contractorweekly.com/contractor-news/tax-a-ir35-news/1306-salary-v-dividend-in-2016-17)
### <span style="font-size: large;"><u>Pension changes abandoned</u></span>
There had been much talk of the Chancellor restricting tax relief on pension contributions.

This caused somewhat of a stir within the contracting circles.  Pensions are still seen as one of the most tax effective means of extracting cash from a Limited company.

So, for now at least, we just need to focus on making any pensions payments prior by the end of the tax year.
