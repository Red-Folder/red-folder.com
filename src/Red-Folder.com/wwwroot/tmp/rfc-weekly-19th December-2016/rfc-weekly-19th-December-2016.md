## .Net Core, React & DevOps
So following on from the last couple of weeks, I've been looking to take my pipeline and use it with TeamCity and TypeScript.

TeamCity (so far) isn't presenting any problems.

TypeScript however did.

This was when trying to use it with Jest.  It took me a while to realise that Jest needed to follow the same transpile pipeline that WebPack was using (fairly self evident once I realised).

Similar to the post [here](https://github.com/facebook/jest/issues/1466), the WebPack pipeline was setup to use the TypeScript compiler to covert from TypeScript to ES6 - then to use Babel to complete the transpile.

My original Jest setup was only using the TypeScript compiler (which would have been find if the tsconfig.json wasn't set to target es6 and preserve the jsx).  When running the jest tests I would syntax errors on "import" statements.

As I say, simple enough once I realised that Babel needed to be invoked on the results of the TypeScript transpile.

(Luckily the above link had a great preprocessor script for doing just that). 

## Blog migration
This is one of those side projects that feels like it has been going on forever.

I decided a while ago that I wanted improve the SEO of my website www.red-folder.com.  One of first steps I wanted to take was to move the historical blogger content to within the site.

To this aim, I created a converter (which can be found [here](https://github.com/Red-Folder/BloggerTransformer)) which took a blogger export and reprocessed it to my own requirements.  This involved producing a markdown and meta (JSON) for every post.  It also included producing a comments export file for loading into Disqus.  Largely this working ok - and getting me part way to a similar documents solution recently adopted by Microsoft.

The migration isn't perfect ... but enough to get it across.

I've now got some manual effort to clean up the meta & markdown - which will need to be done manually.  Hopefully have that all completed this week.

Following that, I will setup automatic redirect on the existing blogger site (which should move the SEO ranking).  I've then got some work to make my blog a lot prettier and more functional - currently "basic" would be a kind desciption.

I'd like to be posting everything from there (including the LinkedIn ROI series) soon into the new year.

Like any side project however, it is easily distracted by real life - but I have some time free over Christmas & New Year so hopefully will get this into a much better position.

## Ethical Hacking progress
Better progress on the [Pluralsight Ethical Hacking](https://app.pluralsight.com/paths/certificate/ethical-hacking) Path this week.

Now 21% through:
![Ethical Hacking Progress](/media/blog/rfc-weekly-19th-December-2016/PluralsightEHPath.PNG)

I've now completed the [Reconnaissance/ Footprinting](https://app.pluralsight.com/library/courses/ethical-hacking-reconnaissance-footprinting), for which I got 9/10
![Reconnaissance Learning Check](/media/blog/rfc-weekly-19th-December-2016/Recon-LearningCheck.PNG)

I also completed the [Scanning Networks](https://app.pluralsight.com/library/courses/ethical-hacking-scanning-networks/table-of-contents) course.  For that I got 10/10
![Scanning Networks Learning Check](/media/blog/rfc-weekly-19th-December-2016/Scanning-LearningCheck.PNG)

##Shameless self-promotion
No new ROI posts I'm afraid.  I've got a final one for this year lined up which should be going out Thursday.  This is nice easy one to end the year on.
