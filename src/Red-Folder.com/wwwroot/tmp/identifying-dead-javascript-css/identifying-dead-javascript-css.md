I've started to look at identifying dead Javascript and CSS from a largish e-commerce site.  It's a site that has been very well built, but has had numerous hands working on it over the years - this means a chance of redundancy Javascript and CSS - thus Technical Debt.

## Why
There are a few reasons why I want to do this:

* Speed - if I remove redundant Javascript and CSS then the downloaded files are smaller - thus faster
* Cleaner code - makes maintenance easier due to smaller code base
* Consolidation of desktop and handheld code

The site currently has two versions - one for desktop, one for mobile.  Behind the scenes there are two sets of files (for part of it) - this includes Javascript and CSS.  Hopefully we should be able to reduce the code base by looking at consolidation.  Would also make moving to responsive design easier in the future if we have a single source for multiple devices.

## Is this difficult?
Yes.

I'd love someone to point me at nice easy way to identify Javascript or CSS no longer in use - but for the life of me I can't see an easy way of doing this.

If only there was an automated tool ... anyone??? please ???

## The process
Ok, while I'm waiting for someone to answer my plea, I'll talk you through my process as it stands.

For each Javascript file (of which there are many):

* Identify all the selectors used (predomination class and ID)
* Identify all the Javascript methods/ fields (very OO structure)
* Search all pages (&amp; Javascript) for where the selectors, methods or fields have been used.  Note that the site is using ASP.Net MVC - so I'm predominately searching views - but also keeping an eye on the controllers - just in case something has sneaked in.
* Compile a massive list of dependancies.  In some cases where the Javascript is being used by a partial view, I will also need to find the dependancies for that as well - basically arrive as a real page.

Ultimately I hope to get to a spreadsheets which tells me on which page Javscript is used.  Hopefully I should have the odd bit of Javascript which has no dependencies - thus is dead code and can be removed.

As I say the code is actually very well written and has historically been maintained well - so I probably haven't got a lot of dead code to find.  This is likely to make it more difficult - the needle in the haystack principal.

As yet, I've spent no time thinking about the CSS - but I'd expect to go down a similar road.## Next steps
Work through the process (at least for the Javscript) and see what I get.
