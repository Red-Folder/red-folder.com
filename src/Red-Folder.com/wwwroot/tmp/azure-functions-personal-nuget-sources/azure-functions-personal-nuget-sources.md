You will quickly find you want more functionality within your Azure Functions that you'll want to code directly.  You'll want to take advantage of Nuget and your own libraries.

If you have your own code, you can compile it up as a dll and drop it into the bin directory of your function, but this can become quite cumbersome if you have code that you want to share between multiple functions.

This is where you will want to use a Nuget server for your own packages.

Now generally you are not going to want to use Nuget.org for your little pieces of code (neither should you) - this is where you are going to want to use your own Nuget server.


### Hosting the Nuget packages
For this you have a few choices; build and host your own Nuget server (loads of documentation available for this) or use a SaaS model.

For my current projects, I'm experimenting with MyGet.org.  This (so far) seems perfect for shared code between my functions.

If however you intend to host your own (you may already have one within your company) then you need to make sure that the Azure Functions VM can access it.  This means making sure that your Nuget server is externally accessible by that VM.  This may require network/ firewall changes on your part - so make sure you understand what is required before jumping at that.

Once you have you package hosted, the next job is add the additional package source to your Azure Function setup.


### Setting up Azure Functions to use the new Nuget Server
When I originally started looking at this, I found very little documentation.  But the work to do it is actually very simple.

In the root of your Azure Function site (same place as your host.json) add a file called nuget.config with the contents similar to the below:

%[https://gist.github.com/Red-Folder/34056d8115eb839f27b427a06cdea313.js]

You can name the source anything you like, but the Url will need to Nuget feed source.

For a practical example. see [here](https://www.blogger.com/(https://github.com/Red-Folder/WebCrawl-Functions/blob/master/nuget.config) where I've configured mine to use my MyGet.org feed.


### Using it
You can then simply reference one of your Nuget packages in the same way as you would any other Nuget package for Azure Functions - you add it as a dependency in your project.json.

An example of this can be seen in my WebCrawlResults function - it use the "Red-Folder.WebCrawl.Data" package (which is simply a model to allow de-serialisation of crawl data).

It can be seen [here](https://github.com/Red-Folder/WebCrawl-Functions/blob/master/WebCrawlResults/project.json).

And that's it.  Nice and easy.
