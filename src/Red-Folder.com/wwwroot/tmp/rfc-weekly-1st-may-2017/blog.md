Another bank holiday weekend - another quite week.

## Blog Re-development
I've been making some improvements to the basic pipeline for my [Docs.Functions](https://github.com/Red-Folder/docs.functions).

On build, VSTS is running through unit tests (with coverage being uploaded to VSTS - via OpenCover & OpenCoverToCoberturaConverter) and running an automated acceptance test.

I've written previously about the automated acceptance test.  I've extended it a little since last time, but basically it performs the same actions:

* Generates a blog name based on the current date & time
* Checks to see if this blog already exists (it shouldn't).  I've added a basic Blog API for the meta data
* Creates a new blog (meta data, blog text & image) and pushes it to [red-folder.docs.staging](https://github.com/Red-Folder/red-folder.docs.staging)
* I then give it 60 seconds to allow the Docs.Function to perform its tasks (I could probably be more efficient if I add some form of webhook)
* Checks again to see if the blog exists

Assuming all works, then its a good indication all is working well.

Plenty of functionality to go yet - but I wanted to spend some time on both the pipeline and the quality of the code.  Already refactored a fair amount of code.  Likely to do more as I work through it.

Hopefully have the core of this completed by end of May to allow me to move on to other development.

## Azure Functions and optional route parameters
Caught out a little by a bug in Azure Functions this week.  Luckily found details of the bug already logged - [Optional route parameters throw exception when they are omitted #1216](https://github.com/Azure/azure-webjobs-sdk-script/issues/1216)

I found that I had to set a default in function.json:

```
"route": "Blog/{blogUrl=*}"
```

Then have some logic within the function to branch based on if "*" was passed in:

```
if (blogUrl == "*")
{
    // Get all the Urls
}
else
{
    // Get one Url
}
```

Not the best of fixes, but will do until the bug is addressed.

## Replacement Laptop
Finally ordered my replacement laptop.  Had hoped to have prior to the Bank Holiday - but probably best not as the wife would have become a laptop widow.

I've gone for something that *should* be ridiculously overpowered for what I need.  A [Dell Inspiron 15 7567](http://www.dell.com/uk/p/inspiron-15-7567-laptop/pd).

![Dell Inspiron 15 7567](/media/blog/rfc-weekly-1st-may-2017/inspiron-15-7567.jpg)

Its designed as an entry level games machine - so has a massive amount of horse power with a quad i7, 16GB memory & 512GB SSD.  This is considerably more that I've traditionally gone for - so hopefully this time I can avoid a lack of resource problems (although I'm already considering throwing another 16GB memory into it).

All in all it seems to come out fairly strongly in [reviews](https://www.notebookcheck.net/Dell-Inspiron-15-7000-7567-Gaming-Notebook-Review.196454.0.html) - even though it is unlikely to win a beauty pageant.

One of the reasons I went for this (other than the stupid level of power) was the discount available by going through the [Dell Outlet UK site](http://www.dell.com/uk/dfh/p).  Not only are they selling at a good reduction for refurb units (full supported), they have a fair level of other discounts that apply from time to time.  Those discounts vary from 10-30% from what I've seen and will be dependant on the cost of the system from my experience.

It does operate on a first come, first served basis - so a specific device may not be available.

From talking to their chat representatives, it appears that inventory is generally updated 3pm & 8pm (UK time).  So if there is a good discount code available, really worth watching for those points if looking for something.  (And of course you aren't in a rush).

Hopefully should have mine tomorrow.  Really looking forward to having a play with it - its been a long time since I've been excited by a laptop.

## Books
Still working through [Design Patterns by the Gang of Four](https://www.amazon.co.uk/Design-patterns-elements-reusable-object-oriented-x/dp/0201633612).  As I suspect before I started to read it, I'm fairly aware of most of the content (so far).  The book itself is a little dry, but still worth the effort.

## Self promotion
I released [ROI of the Planning Horizon](/blog/roi-of-the-planning-horizon) last week.  It feels an important consideration when looking at the true ROI of a Software project.  I plan to back this up with an example this week of how one of my first every Software projects could have evolved over time - and the associated costs.
