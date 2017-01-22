## TL;DR
At time of writing; neither Selenium or Specflow are ready for .Net Core.

So if you want to produce automated Acceptance Tests, stick with full .Net framework project (for now)

## Situtation
I've recently been looking adding automated acceptance tests to a Asp.Net Core website.  My normal tools of choice would be:

* Specflow to define the tests
* xUnit to run the tests
* Selenium (once heavily abstracted) to drive the website under test

A quick read shows that neither Specflow or Selenium are .Net Core ready.  There do appear to be some workaround - but nothing looks particarlly reliable at this stage.  So much so that I'd definitely advise using a full framework (.Net 4.5+) project at this time.

I did however want to make a note of a few links in this post - primarily so I have somewhere to periodically go back to update on current progress.

## Selenium
There is an active pull request [here](https://github.com/SeleniumHQ/selenium/pull/2269) which claims to make Selenium .Net Core ready.  The Pull Request however has been waiting since 12th June 2016 - so don't expect it to be merged anytime soon.

There seems to be some reluctance in merging until such time as the .Net tooling situation is sorted out.  I suspect that this means it won't be until well after Visual Studio 2017 is RTM.

The contributor has however created a nuget package [CoreCompat.Selenium.WebDriver](http://www.nuget.org/packages/CoreCompat.Selenium.WebDriver) - they are seeing this a temporary solution until Selenium accept the PR.

There is an interesting post [here](http://www.dotnetcatch.com/2016/11/23/selenium-with-net-core/) which talks about using the CoreCompat.Selenium.WebDriver to produce tests in Windows and a Linux container.  Definitely interesting stuff - but doesn't feel ready for reliable acceptance tests.

## Specflow
From what I can see, Specflow will always need Visual Studio to operate.  It is relying on the [extension](https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlowTeam.SpecFlowforVisualStudio2015) to convert the .feature file to the .cs file.  I'm a little bit disappointed by that as I'd like to be able to dev without Visual Studio.  Not the end of the world, but useful when haven't got access to full Visual Studio.

Regardless of the IDE; the project isn't yet .Net Core ready.

As can be seen in this [Github issue](https://github.com/techtalk/SpecFlow/pull/649) there is still a fair amount of work to add the .NET Core support.

Again, there is a temporary solution in place - [SpecFlow.NetCore](https://github.com/stajs/SpecFlow.NetCore).  This the author desribes as:

> "SpecFlow.NetCore is just a kludge/bridge until they add official .NET Core support" [Source](https://github.com/stajs/SpecFlow.NetCore/issues/33)

I suspect with some love and attention, this can be made to work.  But that just doesn't seem a wise thing to do with acceptance tests.

I wonder if I'm a bit strange that I'm more concerned about the "bleeding edge" of my acceptance tests than the actual system under test.

I hope to review this subject over the coming months.