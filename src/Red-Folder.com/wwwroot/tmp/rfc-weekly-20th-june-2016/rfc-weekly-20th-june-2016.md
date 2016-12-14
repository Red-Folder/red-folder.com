## [The RESTed NARWHL](http://www.narwhl.com/)
A website aimed at providing API design framework.  Its author assumes you’ve hit a level with your APIs (such as hypermedia linking) and then want to move it further.

Personally I really like the idea of defined schemas for the payloads – that ideally could be used to generate proxies in the client (similar to the purpose that the WSDL served under SOAP services).

Also talks about content negotiation for versioning.

Number of practices here that I’d like to drag into my www.red-folder.com website project (or rather add to the ever growing backlog).

## [OpsDash](https://www.opsdash.com/)
OpsDash describes itself as:
> “OpsDash is a self-hosted server monitoring, dashboarding and alerting solution. It is cluster-aware and can monitor services too. OpsDash is lean, mean and easy to deploy.”

Not had a chance to have a deep look at it yet, but maybe worth investigating as a cost effective monitoring solution.

## [Cloudflare](http://www.cloudflare.com/)
I have to admit to not being aware of Cloudflare until early last week (it was mentioned in a podcast).
Basically it’s another CDN – but two things jumped out from the podcast.

The amount of traffic it handles.  And then how it learns from that traffic to know what is potentially malicious.

According to the podcast, then CDN sees more traffic than Facebook.  A massive amount of traffic goes through it.

And with that, it is able to learn what looks like bad traffic.

So last week I set up www.red-folder.com on it – it took all of about 5 minutes to provision (note that you need to move your DNS – so I had to move away from Azure DNS – little bit of shame, but so far so good).

Once across I can set up security levels:

* Essentially off: Challenges only the most grievous offenders
* Low: Challenges only the most threatening visitors
* Medium: Challenges both moderate threat visitors and the most threatening visitors
* High: Challenges all visitors that have exhibited threatening behavior within the last 14 days
* I'm Under Attack!: Should only be used if your website is under a DDoS attack

And this all works through Cloudflare being able to assign a Threat Score (I assume to IP at the most basic level).

In addition to that (which probably isn’t hugely vital for my site) I’ve got HTTPS, analytics and caching.

All for free (at least for the size of my website)

## [Tinypulse](https://www.tinypulse.com/)
This is a tool I looked at about 18 months ago – and I love the idea behind it.

In summary it’s a staff survey tool that provides a “pulse” of how the organisation is.

I originally had a look at it for the ability to ask anonymous questions of the team – allowing for greater feedback – something that is so easy to forget/ miss.

Yes there are other survey tools – but this one feels much more focused and less intrusive.

It is a constant feedback from your staff – which for me is invaluable.

And it would have to be given the cost.  It really doesn’t seem cheap – and I could see as a difficult sell in most organisations.

I would however still push an organisation to try it.  Even the act of telling people you are investing $10 a month to engage them should have a positive impact on their self worth.

There is an argument to say that this sort of system should be used UNTIL the culture is self supporting.  Once the mentality gets baked in, I’d suspect that you could accomplish the same without the system.

## [Feature Toggle](https://github.com/jason-roberts/FeatureToggle/)
Interesting little library (for .Net, installed by Nuget) to provide Feature Toggling.

Was very interested to see that there was an associated Pluralsight course – but I abandoned the course fairly quickly as seemed overly padded.  Maybe right for someone coming to the concept from scratch – but anyone with even a passing  familiarity of the concept or technology will want to skip straight to having a go.

I’m sure that the same could be accomplished fairly easily by rolling your own – but this does seem a good example of becoming more valuable from community input.  So if you find something missing from the library, fork it on Github, make the change and issue a pull request.  A project like this will only get stronger from more minds on it.

## [Model-View-Controller Explained Through Ordering Drinks At The Bar](https://medium.freecodecamp.com/model-view-controller-mvc-explained-through-ordering-drinks-at-the-bar-efcba6255053#.ytil1f3mw)
An interesting description of MVC as described by ordering a Manhattan

Having known bars where you would have been glassed for that sort of request, you have to wonder if that maps to a 404 (drink not found) or 500 (internal bartender error)

Either way an interesting &amp; fun little read.

## [Octokit.Net video](https://channel9.msdn.com/events/dotnetConf/2016/A-Lap-Around-OctoKitNET)
Not had a chance to watch this yet – but keen to promote given that I’ve a couple of pull requests in the Octokit.net [opensource project](https://github.com/octokit/octokit.net).

Really great project to play with – good structure and good testing.  I learn a few xUnit things from the project.  Definitely one of those examples that are good to help build your personal capabilities by learning from some really good developers.

## Shameless self-promotion
Only one article this week – in my ROI series I’ve looked at a report extolling the virtues of [Microsoft Certifications](https://www.linkedin.com/pulse/roi-microsoft-certifications-mark-taylor).  I declare a bias as I have one – but I did find the underlying report interesting.

Very slowly, I’m still working on converting www.red-folder.com over to .Net Core/ Asp.Net Core – but I’ve somewhat distracted myself with Angular.  It is a framework that I wanted to learn this year (probably along with Angular 2) – so I keep digging further and further into the framework.  At this point I think I have some more work to update the tooling, implement standards as documented in the [Style Guide](https://github.com/johnpapa/angular-styleguide/tree/master/a1) and put proper testing in as covered in this [Pluralsight course](https://app.pluralsight.com/library/courses/play-by-play-angular-testing-papa-bell/table-of-contents).

It is probably fair to say that I’m very much a fan of John Papa.  Having watched the Pluralsight course, I’m also a real fan on Ward Bell’s dress sense (has to be seen).
