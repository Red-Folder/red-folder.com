## .Net Core, React & DevOps
Last week I talked a little about an upcoming project that is likely to involve .Net Core, React & DevOps.

Ahead of that project I wanted to do a simple pipeline using Visual Studio Team Services (actual solution is likely to use Team City).

So I've created a boilerplate solution with two projects:
![Project Structure](/media/blog/rfc-weekly-12th-December-2016/ReactCoreMVC-Project-Structure.PNG)

The ReactCoreMVC project is a default ASP.Net Core MVC project (after some cleanup).  This serves a "Hello World" React App.  That is pretty much all the project does.

The ReactCoreMVC.Tests project is an xUnit project which does a single unit test on ReactCoreMVC.  Remember the aim of the game is to get a pipeline, not build a fully featured app.

So the pipeline looks like this:
![Project Pipeline](/media/blog/rfc-weekly-12th-December-2016/ReactCoreMVC-Pipeline.PNG)

* dotnet restore - does exactly what is says and brings down all the nuget dependencies
* npm install - brings down all of the npm dependencies
* WebPack: ReactCoreMVC - This runs webpack on the React App.  Note I've added a script section into the npm package.json to make this easier
* Test: ReactCoreMVC - The first set of testing that I run.  This uses Jest to run unit tests producing both test results and coverage for the React app.  I had originally done this with Karma/ Mocha/ Chai, but it made sense to switch this over to Jest as it is the Facebook recommended testing framework.  Jest also seems a little easier in the setup.
* Publish Code Coverage Results: Jest - Does what is say, publishes the Jest Code Coverage to VSTS.  Unfortunately there are problems with this ... see below
* Publish Test Results: Jest - Publishes the Jest Test Results to VSTS
* Build: ReactCoreMVC - Back to .Net, we build the website
* Build: ReactCoreMVC.Tests - Note that I find it more reliable to build each project in the right order - otherwise dotnet seems to struggle to find the project dependencies
* Create ReactCoreMVC output folder - This and the next three steps are all to get the xUnit tests & code coverage into an appropriate format to publish.  I'd suspect there is an easier way, but I've yet to find it.  This step just creates an output directory
* Test: ReactCoreMVC - This is where the magic happens.  I'm using a combination of OpenCover, xUnit and dotnet cli to produce both the test results and the Coverage Report.  The task runs the following (I've separated the parameters onto their own line for readability):
```
OpenCover.Console.exe
-oldStyle 
-register:user 
-target:"dotnet.exe" 
-output:"$(Build.ArtifactStagingDirectory)\ReactCoreMVC.Tests\opencover.xml" 
-targetargs:"test tests/ReactCoreMVC.Tests/project.json 
-xml $(Build.ArtifactStagingDirectory)\ReactCoreMVC.Tests\xunit.xml" 
-filter:"+[*]WebApplication.* 
-[*.Test]* -[xunit.*]* 
-[FluentValidation]*" 
-skipautoprops 
-hideskipped:All
``` 
* Generate Coverage Reports - This runs ReportGenerator to take the OpenCover results and produces a HTML report
* Convert Coverage to Cobertura - This runs OpenCoverToCobeturaConverter to convert the OpenCover results into Cobertura format (something VSTS understands)
* Publish Test Results: ReactCoreMVC.Tests - Publishes the xUnit results
* Publish Code Coverage Results: ReactCoreMVCTests - Publishes the OpenCover results (in Cobertura) 
* Package ReactCoreMVC - Packages the website ready for Octopus
* Push Package to Octopus 
* Create Octopus Release: ReactCoreMVC
* Deploy Octopus Release: ReactCoreMVC to Staging - this pushes out to one of my Azure boxes.

This gives me the following summary in VSTS:
![Project Summary](/media/blog/rfc-weekly-12th-December-2016/ReactCoreMVC-Summary.PNG)

Ok, above I alluded to a problem with VSTS and multiple Coverage reports - simply put it doesn't cope with them.  When we talk about Coverage reports, there are generally two parts - the results (metrics, paths, etc) and a report (html representation of those results).

Now VSTS can handle the upload of the multiple html reports ok - is does nothing more that store them as separate sets of files that you can download and reviewed.

Its the results I'm a little more interested in.  That Code Coverage section is only taking into effect the xUnit/ C# tests - it isn't including the Jest/ React tests.  Ideally you'd have a summary which was a combination of both - then be able to drill into the different parts (by technology or function).  The test results actually do this already.  I've raised a [github issue](https://github.com/Microsoft/vsts-task-lib/issues/186) with the Microsoft VSTS task team for this.  I suspect however, I'll need add another pre-processor to the pipeline to merge them prior to publish to VSTS.

As the upcoming project doesn't use VSTS, I don't see this as a major blocker - but it would be nice to have this functionality for other projects.

## Ethical Hacking progress
Limited progress on the [Pluralsight Ethical Hacking](https://app.pluralsight.com/paths/certificate/ethical-hacking) Path this week.

Rather disappointingly, it appears I've only got to 11% through (sure I've spent more time that than - possibly not updating correctly):
![Ethical Hacking Progress](/media/blog/rfc-weekly-12th-December-2016/PluralsightEHPath.PNG)

I'm still on the [Reconnaissance/ Footprinting](https://app.pluralsight.com/library/courses/ethical-hacking-reconnaissance-footprinting) course.  So far a lot of the "Reconnaissance" is similar to basic activities I might carry out when looking at a company ahead of an interview (history, direction, technologies, etc).  I'd personally not see a lot of this information as a security problem - I've never believed in security by obscurity.  I do however understand the principal of the harder you make the task, the more work you make for the hacker and increase that investment they have to put in (may deter eventually).

##Shameless self-promotion
Last week I published [ROI of Outsourcing](https://www.linkedin.com/pulse/roi-outsourcing-mark-taylor).  Off the back of a great article by Troy Hunt, I wanted to take a look at Outsourcing from an ROI perspective.

I very much agree Troy when he says:

> “if you're looking at hourly rate as a metric for outsourcing success, you're doing it very, very wrong!” [Troy Hunt](http://www.troyhunt.com/offshoring-roulette-lessons-from-outsourcing-to-india-china-and-the-philippines/)

I've seen too many people see a day/ hour rate as being the deciding factor without properly looking at the side effects that can occur.  While I do believe that Outsourcing can be a beneficial practice (and possibly even cost saving in some instances), I'd advise going in with your eyes open.
