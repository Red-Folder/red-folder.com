Working with GitHub and Azure functions is fairly easy.

By default, Azure Functions doesn't provide any version control - you make a change in the portal and the prior version is lost.  And if you accidentally delete the function, it is all gone.  Thus source control is super important.

There are only two things to consider:


* The Tree
* How to make changes


### The Tree
If you use Kudu (Function App Settings -> Go to Kudu), you may be a little confused what you should download to produce your source controlled version of the functions.
You will want to download every that is within Site/wwwroot (I generally download the wwwroot, then put the contents into the source controlled repository on my local machine).
What you should have in you git controlled folder is host.json and then a folder for every function that you have.
An example of what you are after can be found in my WebCrawl-Functions (https://github.com/Red-Folder/WebCrawl-Functions) repo.
Once you have you code in GitHub, then just enable Continuous Integration (Function App Settings -> Configure continuous integration).  Azure will then pickup changes to the repo and deploy automatically.

### How to make changes
Possibly a obvious statement, but one I make to be clear, once you are using continuous integration you can no longer make changes to your functions via the portal - everything has to be through the GitHub.
This can be a little frustrating when making minor changes.
You'll find that you'll want to setup your development environment locally for testing/ debugging Azure Functions to make this process a lot smoother.  This Microsoft article() talks about how to set up an environment to test locally (although you will still need access to Azure).

Its still a little cumbersome, but saves waiting for the push, sync, deploy, recycle loop just to test a 1 character change.
