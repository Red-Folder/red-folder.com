## Scientist.Net
Really like the looks of this project from Phil Haack - [Scientist.Net](https://github.com/github/Scientist.net)

It allows for running two versions of code (in live) and comparing the results to each other.  It's aimed at making refactoring safer by allowing you to ensure that your new path is producing the same results as the existing - while only the existing is used.

## Integration Testing with .Net Core
This [Integration Testing article](https://docs.microsoft.com/en-us/aspnet/core/testing/integration-testing) provides advice on running integration tests for ASP.Net Core.  It introduces the Test Host - a host specifically for integration testing.

## EF Core Testing
This [Testing with InMemory article](https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory) shows how to use the InMemory provider with EF Core for integration tests.

## Blog migration
I've started work on splitting the blog "content" from the red-folder.com website code (although this is likely to be put to one side due to other commitments).

Part of the intended solution is to trigger the publishing off a Github repo change (where I will store the content).

Now I wanted to put into place a BDD (SpecFlow) end to end test that would simulate the entire process.  To do this I wanted to be able to automatically generate a fake blog into a test repo.

This [GitHub Commit article](http://laedit.net/2016/11/12/GitHub-commit-with-Octokit-net.html) was perfect for showing me how to do it.

I now have a SpecFlow test, hooked into the [docs.functions](https://github.com/Red-Folder/docs.functions) VTST build process, which generates a fake blog into [red-folder.docs.staging](https://github.com/Red-Folder/red-folder.docs.staging) repo.

It doesn't do a lot more than that at the moment, but it's a great starting point.

## Bot Prevention
My progress to date with the Ethical Hacking learning path came in handy this week.  I've been looking to provide advice to a client on how to defend against Bot abuse.

As with most things security, the more I look the more concerned I get.

I already knew that there where services out there to solve captcha like bot checks for you.  What I hadn't realised was just how easy and cheap it was.  We are talking sub pence for a captcha response.  If you do a goolge search for captcha solvers you will find a bus load.  And they all look like legitimate services.  Scary stuff.
![reCaptcha](/media/blog/rfc-weekly-8th-february-2017/recaptcha.png)

In my advice, I talk about how to think like a bot master - how much will your defence inconvenience me? 

I summarise the advice with the following facts:

* If it can be done by a human, it can be automated by a bot
* Attacks will have a financial ROI – cost of the bots vs the rewards
* Running a bot can be exceptionally cheap
* More security barriers, the more expensive that becomes
* Attacks can’t be stopped – just made too expensive to be viable
* It’s an arms race

I suspect I'll use some of this information on a second ROI article on security in the future.  All good (but scary) stuff.

## Ethical Hacking progress
Ok, another big jump in progress completed (helps if I don't update the blog for a while).

I still may struggle to complete by the end of the month, but it's looking close.  Now at 82% of the [Pluralsight Ethical Hacking](https://app.pluralsight.com/paths/certificate/ethical-hacking) Path.
![Ethical Hacking Progress](/media/blog/rfc-weekly-8th-february-2017/PluralsightEHPath.PNG)

Breakdown of courses covered;

[Hacking Web Servers](https://app.pluralsight.com/library/courses/ethical-hacking-web-servers)
![Hacking Web Servers](/media/blog/rfc-weekly-8th-february-2017/HackingWebServers-LearningCheck.PNG)

[Hacking Web Applications](https://app.pluralsight.com/library/courses/ethical-hacking-web-applications/table-of-contents)
![Hacking Web Applications](/media/blog/rfc-weekly-8th-february-2017/HackingWebApplications-LearningCheck.PNG)

[SQL Injection](https://app.pluralsight.com/library/courses/ethical-hacking-sql-injection/table-of-contents)
![SQL Injection](/media/blog/rfc-weekly-8th-february-2017/SQLInjection-LearningCheck.PNG)

[Hacking Wireless Networks](https://app.pluralsight.com/library/courses/ethical-hacking-wireless-networks)
![Hacking Wireless Networks](/media/blog/rfc-weekly-8th-february-2017/HackingWirelessNetworks-LearningCheck.PNG)

[Hacking Mobile Platforms](https://app.pluralsight.com/library/courses/ethical-hacking-mobile-platforms/table-of-contents)
![Hacking Mobile Platforms](/media/blog/rfc-weekly-8th-february-2017/HackingMobilePlatforms-LearningCheck.PNG)

## Contractor Process
Started to update by CV in line with my normal renewal process.  I'll also be making some updated to the website & LinkedIn over the next week or so.
