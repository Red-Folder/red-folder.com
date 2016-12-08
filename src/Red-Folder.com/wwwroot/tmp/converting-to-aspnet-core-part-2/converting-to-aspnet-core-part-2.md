In the [first article](http://red-folder.blogspot.co.uk/2016/03/converting-to-aspnet-core-part-1.html) in this series, I've made a empty ASP.Net Core project with MVC 6 set up and ready to go.

In this article, I'll be looking to copy content from my existing ASP.Net MVC 5 project.

### Steps I missed
I'm using pre-release libraries (pretty much everything) - but I forgot to set the Include Pre-release checkbox on the NuGet Package Manager. Oops.

I missed adding _ViewStart.cshtml - without this I wasn't getting the layouts pulled in.

### Steps
So these are the steps for this article that I've taken:


* Copy images from old project into wwwroot\images
* Copy site specific JavaScript into wwwroot\scripts
* Copy site CSS &amp; Less into wwwroot\css
* Update Less files for background URLs (location changes)
* Copy existing models into ViewModels - I've chosen to use ViewModels to temperate from Service (or BLL) models - seems to be good practice
* Update models to reflect ViewModels in namespace
* Copy existing Views
* Update Views to reference the approriate ViewModels
* Update Views to remove Bundle renders (replace with direct loading of css/ js)
* Update Views to replace @Html.ActionLink and @Url.Action with tag helpers (see below)
* Update Views to replace glythicons to Font Awesome
* Copy any Service classes in Services folder
* Update any Services class namespace references
* Copy existing Controllers in Controllers\Web
* Update any Controller class namespace references
* Replace "System.Web.Mvc" with "Microsoft.AspNet.Mvc"



One thing you will notice is that bundling &amp; minification has disappeared.  We are going to use Gulp for these, but to keep this article simple I'll set that up in the next article.

### Tag Helpers
<blockquote class="tr_bq">"Tag Helpers are a new feature in MVC that you can use for generating HTML. The syntax looks like HTML (elements and attributes) but is processed by Razor on the server. Tag Helpers are in many ways an alternative syntax to Html Helper methods but they also provide some functionality that was either difficult or impossible to do with helper methods." [MSDN](https://blogs.msdn.microsoft.com/cdndevs/2015/08/06/a-complete-guide-to-the-mvc-6-tag-helpers/) </blockquote>In my case, I've replaced @Html.ActionLink &amp; @Url.Action with <code> &lt;a asp-action="Index" asp-controller="Home" ...</code>
<code>
</code>Definitely looks a lot cleaner in the HTML.

### Html.RenderPartial
In RC1 it appears that Html.RenderPartial hasn't been implemented.

Apparently sorted in in RC2 (see this stack overflow [answer](http://stackoverflow.com/questions/34640754/where-did-renderpartial-go-in-asp-net-5-mvc-6)).

For now, we just amend to Html.RenderPartialAsync.

### Less
I use less for my CSS.  ASP.Net Core moves you towards using a build tool to compile your less file to css.

This seems to be aimed at producing better tooling for Continuous Integration pipeline.

The two favourites seem to be Gulp and Grunt.

Both are NPM (node) based build tools and can accomplish much the same tasks.  In my case I'm likely to use the build tool for compiling less to css, TypeScript to JavaScript, minification, bundling, etc.

So which tool to go for?

A quick read of comparisons on the internet indicate that there is a lot of parity between the two.  Ultimately it seems that your main choice is between configuration (Grunt) and code (Gulp).
Being a developer at heart, Gulp is my first choice.

For this article, all I need it to do is compile my less files.  For this:

Add NPM Configuration (package.json).  To which add the following the devDependancies:


* "gulp": "~3.9.1"
* "gulp-less": "~3.0.5"


Add the gulpfile.js - see the check-in for the basic setup

This seemed enough to get me going.  This Asp.net [article](http://docs.asp.net/en/latest/client-side/using-gulp.html) talks you through using Gulp.

### Check-in 
Ok, at this point I'm able to compile and run the website.  All looks file.  You can see it in this [commit](https://github.com/Red-Folder/red-folder.com/commit/dcb2bfb6d1a7f0f3ea5b2c337e79d600c3b37a91)

### Next
In the next article I'm going to look at using more Gulp goodness to minify and bundle.