## Web.config Transformation Tester
Asp.Net Web.Config transformations can be a tricky subject to get right.  This [online tool](https://webconfigtransformationtester.apphb.com/) provides a simple interface to test your transforms.

Great stuff.

## Free eBooks from Microsoft
Lots and lots of free eBooks announced in this [blog post](https://blogs.msdn.microsoft.com/mssmallbiz/2016/07/10/free-thats-right-im-giving-away-millions-of-free-microsoft-ebooks-again-including-windows-10-office-365-office-2016-power-bi-azure-windows-8-1-office-2013-sharepoint-2016-sha/)

I can't comment on the quality (some look questionable) - but there a good number of subjects covered and you're likely to find something for most technical bias.

## Update on Writage.com
I wrote last week about [Writage](http://www.writage.com) - a plugin for Word to handle Markdown.

Despite my initial joy - I've found it really doesn't work for me a well as Notepad++ or VS Code.

Doesn't seem to handle images for example.  Maybe worth keeping any eye in the future though.

## Lighthouse
A tool from the Google Chrome team - a [plugin](https://chrome.google.com/webstore/detail/lighthouse/blipmdconlkpinefehnmjammfjpmpbjk) which provides performance and quality advice on your app.

As a plugin, you can use similar to other validation and performance tools.  

Behind the scenes however is a CLI tool which I believe can be plumbed into your Continuous Integration pipeline.

Definitely one I'll have a play with over the coming weeks.

## Browser Pre-fetching
An [article](https://css-tricks.com/prefetching-preloading-prebrowsing/) from CSS Tricks looking at resource pre-fetching.

> "Pre-fetching is a way of hinting to the browser about resources that are definitely going to or might be used in the future, some hints apply to the current page, others to possible future pages.
> As developers, we know our applications better than the browser does. We can use this information to inform the browser about core resources." [Patrick Hamann](http://patrickhamann.com/workshops/performance/tasks/2_Critical_Path/2_3.html)

## CSS Triggers
Useful [Cheatsheet site](https://csstriggers.com/) which covers how the major browser engines will handle specific CSS attributes.

This can be especially useful when applying CSS styles to an already rendered page.  Reducing the amount of change can greatly help the user.

* _Layout_ is where the browser will need to change the layout of the page due to the attribute affecting the geometry of the element - and consequently, related elements.  It is the most traumatic.
* _Painted_ is where the browser will need to repaint any pixels
* _Composite_ is where the browser will need to stich the pixels together.  It is the least traumatic.

In theory, Layout will be the most traumatic.  Composite the least.

## Ethical Hacking progress
I’m now 93% of the [Pluralsight Ethical Hacking](https://app.pluralsight.com/paths/certificate/ethical-hacking) Path.
![Ethical Hacking Progress](/media/blog/rfc-weekly-13th-february-2017/PluralsightEHPath.PNG)

I’ve completed the following since last week:

[SSCP®: Cryptography](https://app.pluralsight.com/library/courses/sscp2015-cryptography/table-of-contents)
![SSCP®: Cryptography](/media/blog/rfc-weekly-20th-february-2017/SSCPCryptography-LearningCheck.PNG)

## Contractor Process
Following on from last week; It appears adding my Agency Note at the top of the CV has had the desired result.

I've had calls; but they have been considerably more relevant than I've previously experienced.  I'll use that approach again.

I've made some changes to the overall design of my website - partly to revamp it - partly because I'd like to offer out a consultancy product through it (more on that in the future).

If I'm honest, I'm not that happy with the look 'n' feel.  Not being a designer really shows out.  Maybe an excuse to spend some time on design principles.

Normally about now I'd start reaching out to my network - especially over LinkedIn.  Given however that a renewal is currently underway, then I'll not be doing that.  Should for any reason that renewal fall through, then I'd restart at that point.
