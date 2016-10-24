RFC Weekly - a summary of things that I find interesting. It is an indulgence; its the weekly update that I would like to receive. Unfortunately no-one else is producing it so I figured I best get on with it. Hopefully someone else will also find useful.

Developing apps in Windows Containers using Docker
--------------------------------------------------
Last week I attended another excellent meetup hosted by DotNetNorth. During the meetup Naeem Safraz gave an excellent introduction to containers in general and the capabilities of the Windows container using Docker.

The talk was focused on developers so predominately covered getting a Windows Container spun up for dev and then getting into a repo. Later, production type concerns of orchestration and management where left out of the talk which really helped to focus on the dev side. Naeem provided the following links if you want to see the content:

* [Slides](http://naeem.cc/WindowsContainers-Slides)
* [Demos](http://naeem.cc/WindowsContainers-Demo)

Azure UK Datacentres
--------------------
Azure have finally brought online some UK regions.

We have two at the moment - UK West & UK South.

They don't however have parity (yet) with North/ West Europe. They are certainly missing key features that I'm playing with at the moment like Azure Functions and Document DB. Hopefully this will change shortly.

You can compare the offerings between regions [here](https://azure.microsoft.com/en-gb/regions/)

Complete Guide to Flexbox
-------------------------
Flexbox has (and still is) a long time in the making. But we seem to be slowly getting there.

Flexbox is being added to the CSS spec and helps greatly with the layout of web pages. It should make things like centring a doddle.

The idea of Flexbox has been around for years ... but the actual specification has taken a long time to be ratified ... and various changes have occurred over the time.

For most people however (including myself) the only really important thing is when will the browsers to support. A check on CanIUse shows we still have problems on IE.

I'd argue at this point that it probably puts Flexbox off the table for most development just due to the IE problems. As time moves on it becomes less of a problem.

The guide can be found [here](https://css-tricks.com/snippets/css/a-guide-to-flexbox/)

How HTTP/2 will speed up the web
--------------------------------
This [article](https://kinsta.com/learn/what-is-http2/) takes a look at the advantages coming out of HTTP/2 - the next evolution of protocol that the web runs on.

The article is a gentle introduction to some of the concepts and is far from technical.

<a href='https://kinsta.com/learn/what-is-http2/'>
		<img src='https://kinsta.com/wp-content/themes/kinsta/images/learn/what-is-http2/similarities_with_http1_spdy.jpg' alt='Similarities with HTTP1 and SPDY' width='830' border='0' />
</a>

From my perspective, I can really seeing HTTP/2 helping with website performance. It handles files in a different manner to previous versions which should improve website load performance. It should also allow developers to move away from a lot of the hacks that we currently have to worth through to get the best performance we can.

Unfortunately, it is unlikely to be a technology that I look at anytime soon due to the Azure support. The below (taken from this thread here) doesn't set any real timescales:

![Azure Web HTTP2 Support](/media/blog/rfc-weekly-17th-October-2016/azure-web-app-hhtp2-support.png)

Shameless self promotion
------------------------
Within my ROI series, I've added the second part looking at Scrum. It can be found [here](https://www.linkedin.com/pulse/roi-scrum-part-2-advice-mark-taylor)

In other news, I've started to move my blog within the website. I'm slowly working through it. This should make the second posting. I've mainly been concentrating on providing the appropriate markup for Twitter & LinkedIn.

Over the next few weeks I hope to migrate all my legacy blogger content.


