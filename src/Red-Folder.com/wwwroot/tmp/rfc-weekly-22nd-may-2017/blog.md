## IntegrationFact
I've long been a fan of SpecFlow.

One of the frustrations for me though is that it would generate any subsequent tests as unit test (as a Fact in xUnit).  This has the unfortunate side-effect that all SpecFlow tests will show up and run within Test Explorer.

While good that those tests show up - generally I'm using SpecFlow for long running or integration tests.  As such, they soon make running regular unit test runs prohibative (nobody wants to run unit tests between each change if they take 60 seconds to run).  For TDD purposes they need to be lightening quick.

I know it is possible use Categories and Traits to separate tests - but I find this somewhat clunky ... and very easy to forget to diable.

Thus I've created to NuGet packages - one for a custom IntegrationFact attribute and one which gets SpecFlow to generate tests using it.

Both are available on NuGet in an alpha state at the moment.  They can be found:

* [RedFolder.xUnit.IntegrationFact](https://www.nuget.org/packages/RedFolder.xUnit.IntegrationFact/)
* [RedFolder.xUnit.IntegrationFact.SpecFlow](https://www.nuget.org/packages/RedFolder.xUnit.IntegrationFact.SpecFlow/)

First attempt at uploading my own code onto NuGet so it will be interesting to see what (if any feedback) I receive.

## Value of BDD & TDD
While I'm a great believer in TDD (or at least unit tests - how you arrive at them, I'm a bit more flexible) - but a couple times this week have demonstrated that they aren't enough when developing Feature Slices.

Have BDD tests in place to validate that the overall behavior hasn't changed is a time saver.

Knowing that your changes has broken the BDD tests (and thus CI build) quickly, allows resolution to happen in minutes rather than days if left until much later in a project.

## Self promotion
Released [ROI of the Planning Horizon - The affect](/blog/roi-of-the-planning-horizon-the-affect) last week.

This hopefully provide a tad more clarity over the last few articls.  In it I will revist previous articles and provide comment based on having a longer term mind-set.