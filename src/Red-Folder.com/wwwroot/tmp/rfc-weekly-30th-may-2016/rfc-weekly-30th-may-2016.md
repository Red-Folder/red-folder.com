## Specflow Cookbook
I've recently introduced Specflow into a client.  If you've never come across Specflow, it is a BDD (Behaviour Driven Development) tool for .Net.

It uses a Gherkin style language:

![Image](/media/blog/rfc-weekly-30th-may-2016/GettingStarted-FirstFeatureFile.png)

I will then generally use it with xUnit as a test runner.

You can find more on Specflow [here](http://www.specflow.org/)

The [Specflow Cookbook](http://specflowcookbook.com/) is a great collection of recipes to help you get started.

## [Regular Expressions](http://www.regular-expressions.info/)
When using Specflow, I've found having a great regular expression website to hand is a godsend.  Of course generally useful to have anyway.

## [Prefix.io](http://www.prefix.io/)
This is one of those tools that I post here so I remember to try it (or more likely have the link when I need it).

Prefix.io provides a lot of insight into your code – stuff likes bugs, performance, etc.  It’s aimed at being used in development rather than production.  But that is because it is being provided free by Stackify – who want to sell you their production version (can’t blame them – and I really like this free for dev approach).

I've added myself a Trello card to try it out on my own site.

## [Postman](https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop?hl=en)
My new favourite Chrome extension (remember when that was a thing?)

Postman is a tool that provides a test client for REST APIs.  Really nice clean interface allowing you to save queries and share them with your team.

I've found this an easier tool for REST calls than Fiddler or SOAPUI.

## Xamarin
I've been having a bit of a play with Xamarin on Android this week.  Not gone very far into it, but the Android implementation is very similar to the native development.

This should (in theory) make transition for me fairly easy.  I've played around with native Android for many years (thus my Cordova Android plugins) – so the idea of being able to apply all of that knowledge and C# goodness really done appeal.

Plus it looks like the Microsoft virtual devices work really really well (they boot on my aging laptop – which is a significant improvement on the Android SDK).

## Shameless self-promotion
First up, within my ROI series on LinkedIn, I've taken a look at the[ ROI of well written code ](https://www.linkedin.com/pulse/roi-well-written-code-mark-taylor)

I have also created a new repo - [Penetration Dns Server](https://github.com/Red-Folder/PenetrationDnsServer/tree/master)

A Custom Dns server intended for Penetration Testing

This work-in-progress project intended to help with tracing a penetration vulnerability identified by a 3rd party checker.

The checker worked by triggering a DNS query against a custom DNS entry (via an Oracle SQL Injection attack in this case). The site in question had no Oracle, thus I'd expect that the vulnerability was likely being triggered by a 3rd party (maybe a tracking script?).

To be able to investigate I created a custom Dns Server which could be used for analysis.

While now disabled, I had this linked into my Dns as a Nameserver for pentesting.red-folder.com.

This allowed me to perform a nslookup against {random}.pentesting.red-folder.com. Any lookup would then be broadcast out to anyone registered to the service (via SignalR).

I build a very basic listener against the red-folder.com website.

Technically the solution all seemed to work.

Never managed to recreate the actual vulnerability on the site in question - which somewhat makes me question the 3rd parties tools &amp; processes.

Technologies used: Azure DNS, Azure Cloud App Worker Role and SignalR
