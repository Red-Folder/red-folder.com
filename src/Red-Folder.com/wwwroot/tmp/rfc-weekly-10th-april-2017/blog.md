## CSS Learning Path
Back in my state of the nation [article](/blog/rfc-weekly-13th-march-2017); I aimed to complete a number of things by the end of March.

The [CSS Learning path](https://app.pluralsight.com/paths/skills/css) on Pluralsight was one of those (I touch on the others below).  I've now completed - nicely into the expert category.
![CSS Learning Path](/media/blog/rfc-weekly-10th-april-2017/CssLearningPath.png)

As I said last time; a really good refresher.

## www.red-folder.com redesign
A little behind target, the site has now has had a complete facelift.  Had hoped to have completed by end of March (as per the state of the nation) - but due to other commitments, it fell into early April.

Am I finished with it?

For now, yes.

There is definitely more work that needs to be done at a technical & design level.  The underlying code is poor to say the least and needs a fair investment in refactoring.

I'd also like to look at alternative layout technologies.  I'm currently using Bootstrap 3 - but it has limitations when it comes to grid style layouts.  It may be time to consider dropping Bootstrap (even with 4 round the corner) in favour of CSS native solutions.

While I would probably have plumbed for Flexbox ... that has never really got off the ground.  It should be great, but doesn't seem to have the popularity.

The new kid of the block [CSS Grid](https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Grid_Layout) however seems to be gaining traction at a lightening pace.  It's is very new - but seems to be getting browser adoption very quickly.  It seems to be across all the evergreen browsers (see [Can I Use](http://caniuse.com/#feat=css-grid)) - the Android Browser seems to be the outlier to me.  Along with IE & Edge supporting older versions.

I'm likely to spend time on the site ahead of any contract end (it is a marketing tool more than anything else) - so maybe early June - by that point CSS Grid maybe getting better adoption.

## Software Development Healthcheck product
Bit behind on this ... hopefully have on the site during this week for comment.

Interestingly there is a Troy Hunt course released this week on Pluralsight that looks at [Branding](https://app.pluralsight.com/library/courses/play-by-play-crafting-a-brand-for-growth-and-prosperity/table-of-contents).  I'll definitely try to fit some time in for that this week.

## TypeScript
Speaking of Pluralsight courses ....

Following completion of the CSS ones, I went straight into Typescript.  I've made good progress and covered off:

* [A Practical Start with TypeScript](https://app.pluralsight.com/library/courses/typescript-practical-start)
* [TypeScript In-depth](https://app.pluralsight.com/library/courses/typescript-in-depth)

I've one more to do - [Advanced TypeScript](https://app.pluralsight.com/library/courses/typescript-advanced/table-of-contents).  I'll then probably look at Angular 2 or React - with a view to doing something practical.

Not bad considering I expected Typescript to take me into May - but so far, it all looks simple enough.  Practical use may re-educate that view.

## Books
Finally completed [Crucial Conversation](https://www.amazon.co.uk/Crucial-Conversations-Tools-Talking-Stakes/dp/0071401946).  Really not sure why it took so long to read.

The overall message was good - and something that I took a great amount of value from.  I think anyone that can be involved in non-trivial conversation (which is all of us) - could take some value from the book.  I doubt that I'll commit the processes and techniques in the book to heart - but I do see it being a book to come back to following bad conversations.

A colleague referred to this as conversation retrospective - which was a great term.

The simple fact of thinking about conversations more should help (something eluded to by one if the authors).

I've been motivate now to move onto more code based reading.  For some reason I've never got round to [Refactoring](https://www.amazon.co.uk/Refactoring-Improving-Design-Existing-Technology/dp/0201485672/ref=sr_1_1?ie=UTF8&qid=1491824100&sr=8-1&keywords=refactoring) by Martin Folwer - now is the time.

If you compare to Crucial Conversations, which I'd been reading for about 5 months, I'm 30% through Refactoring and have been reading for less than a week.
 
## Blog Re-development
Now I've largely got the website re-design out of the way, it back to the Blog Redesign.

To recap, I wanted to move the blog content into a separate GitHub repository (currently they are part of the same GitHub repo as the website).  I'd like to have an automated pipeline to take any check-in to that Repo and deploy the blog into the website.  This is similar to how Microsoft are doing their documentation pipeline.

I'm planning to mainly use Azure Functions for this.  I started a couple of months back, but pushed to the back burner when I realised I had too many active projects.

I'd like to complete by end of April ... but that maybe optimistic.

## Self promotion
Finally released part one of my mini-series looking at the [ROI of Dependencies](/blog/roi-of-dependencies-part-1)

This seems to have been a mammoth effort.  I started this article back in November 2016.  Originally it was to be a response to imagined questions.  This grew into the Town Hall meetings and following Fiona and team through those early days.

I've received some positive comments from it, so it will be interesting to see how well the 2nd and 3rd parts are received.  I'll be putting them out over the next two weeks.  Releasing everything in one article just was way too large.
