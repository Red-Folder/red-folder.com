## www.red-folder.com redesign
As discussed last week, I'm mid-migration on changing the design of the site.

I've managed to get across quite more than expected, and the additional sections have been done since last week:

* [Services](/Services)
* [Blog](/Blog)
* And the individual blog pages

I've also managed to tidy up a few bugs that I'd not noticed previously.

All in all, I'm happier with the design.  It is still is a long way from where I would like it to be - but it is better, now I would just to get it consistent before taking much further.

## Broken build tasks
Last week I mentioned that I'd lost CSS on the home page.  I tracked this down to a gulp task failing during the .Net Publish task (gulp is called in as part of the pre-publish).

The gulp task failed, not building the CSS bundle - but the Visual Studio Team Services build task continued.  Thus published an incomplete site.

I've noticed something similar in the past that fails in Publish pre/ post scripts don't appear to be failing the entire publish (and thus the build).  So this will need to be something I come back to in the future as this is obviously quite a problem.  For now though, I'll just keep an eye on it during publish.

## CSS Learning Path
All wrapped into the redesign, I'm revisiting my CSS & design skills.

I've watched two courses from Pluralsight last week:

* [Visual Design for the web](https://app.pluralsight.com/library/courses/visual-design-web-2465)
* [Responsive Web Design](https://app.pluralsight.com/library/courses/responsive-web-design)

Both are good courses.

The Visual Design for the web course is a fairly traditional look at web design and theory using Photoshop.  While I'm unlikely to ever design in Photoshop, the design theory was very useful and exactly what I was looking for.  Few really interesting links I picked out of the course:

* [Contrast Checker](http://webaim.org/resources/contrastchecker/) - A tool for evaluating the contrast between two colour - with the aim of improving readability.  I've yet to run this against www.red-folder.com.
* [Visual Hierarchy](https://medium.com/designed-thought/8-ways-to-add-visual-hierarchy-8820be73218a#.4n09h7jve) - A really simple demonstration of visual hierarchy patterns.  I'm using it to understand how to set out content within whitespace (anyone paying attention should be noticing a lot more whitespace in the www.red-folder.com)
* [Web Accessibility](http://webaim.org/resources/designers/) - A series of tips to watch for in making your site as accessible,  Again something I need to go through with www.red-folder.com in mind.

The Responsive Web Design course, while maybe a little long in the tooth now, has some really good content.  One thing that really stood out for me, which wasn't particularly Responsive Web Design based, was the idea of Style Prototype page.

The Style Prototype page is a real page in your app in which you showcase the styles (CSS, HTML, etc) for your site.  Similar to something like the [Bootstrap Components page](http://getbootstrap.com/components) - in which as a developer you can see what styles you have to play with.

This also gives you an ability to validate style choices across multiple devices by validating that one page.  (Ok, that isn't going to catch everything, but should get you most of the way there).

So Style Prototype page is something I'll be adding to the backlog.  Perversely enough, it will likely be added after this current round of redesign - but it will be useful to have for subsequent iterations.

One question that came out of watching the two course was where to do the design.  The Visual Design course was very much what I'd consider traditional Photoshop work - separate images for different device sizes.  Whereas the Responsive Web Design advocated designing in the browser (through code).

I have to admit that the latter of those two sits easier with me as a developer.  It certainly feels like a better fit when working with a responsive web site.  But will generally require multiple skills to achieve (design, CSS, HTML - maybe even JavaScript).

It obviously isn't a question that has definitive answer - but rather a better answer based on the situation.

## Self promotion
Back into releasing ROI articles.  Last week I released [ROI of an analogy](/blog/roi-of-an-analogy) - which had the sub-heading of "Why Software Development isn't like building a house".

The subject (specifically the comparison to building a house) has been something I've been thinking about for a while now.  I've had a number of situation over the years where an incorrect comparison is drawn between the two activities - incorrect to the point where the other party is making wildly wrong assumptions and then expecting those assumptions to be met.

If I'm honest, my initial reaction to the comparison was to argue against its use.  After consideration however, there are useful comparisons that can be drawn - as long as everyone understands that they are not 100% universal.  Thus I went down the analogy route - looking at the positives (and negatives) of using analogies when discussing Software Development.  It feels like I've touched the iceberg on that - likely to be something I revisit in the future.

This week I'm hoping to release an article regarding Technical Debt.  In hindsight, it seems a little odd that 13 months and 33 articles after I started the series that I focus on Technical Debt - it seemed something I would have done much earlier - in fact I even added it to my backlog 9th February 2016.  But it has just never seemed the right time to do it.  Until now.  It feels like the natural successor to the [ROI of an analogy](/blog/roi-of-an-analogy) article.
