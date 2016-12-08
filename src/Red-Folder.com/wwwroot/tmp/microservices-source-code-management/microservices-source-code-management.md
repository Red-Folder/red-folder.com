In my [last post](http://red-folder.blogspot.co.uk/2015/06/microservices-practical-use.html) I started to set the scene for my Microservice work.  In this post I shall be concentrating on how I shall be managing the source code.

For any development project, it is important to setup your source control.  This should be the first step in any development project.

I plan to discuss how I want the source management to work and how I'm going to achieve it.  In the next article I'll show how to ensure that everything keeps in sync.

### The Projects
For the purposes of this article, I will be using three projects:

* MS-Common-Library - a class library for some common code (name gives it a way a little).
* MS-Base-WebAPI - another class library, this time defining a base for my WebAPI Microservices - allows me to provide consistency through the created Microservices.
* MS-Projects - my first Microservice.  This Microservice will perform double service in this article.  More on this later - for now just assume its an implementation.

All of the projects will be developed in Visual Studio.  Three solutions will be used:
![Image](/media/blog/microservices-source-code-managementProjectSummary.png)

The MS-Common-Library solution will consist of only the MS-Common-Library project.  The MS-Common-Library project will also be used by the MS-Base-WebAPI and MS-Project so it is included in their solutions as well.
It should be explained at this points that I am not planning for each solution to reference the same file version of the project.  Each solution will have it's own copy of the project.  See the next section on versioning.
The MS-Base-WebAPI solution will consist of the MS-Common-Library &amp; MS-Base-WebAPI projects.
The MS-Projects solution will consist of the MS-Common-Library, MS-Base-WebAPI and MS-Projects projects.
### Versioning
At this point its important to note the versioning allows for the solutions to have different versions - not to force versions down onto dependent projects.  This allows the project teams to decide when (and if) to take newer versions for the dependent projects.  This keeps the dependent projects loosely-coupled.
So here's an example of what I'm trying to achieve;
We will focus with the MS-Common-Library.  When we start, all are version 1:![Image](/media/blog/microservices-source-code-managementProjectVersioning01.png)

We update MS-Common-Library (via the MS-Common-Library solution) to version 2:![Image](/media/blog/microservices-source-code-managementProjectVersioning02.png)

At this point we choose to pull those changes into the MS-Base-WebAPI - updating it's MS-Common-Library to V2.  Note that we choose not to pull the changes into MS-Projects - there is no need and could stay this way for week, months or even years (or until the relevant project team get round to it):![Image](/media/blog/microservices-source-code-managementProjectVersioning03.png)

We now decide to make further changes in the MS-Base-WebAPI solutions version of MS-Common-Library - effectively creating Version 3:![Image](/media/blog/microservices-source-code-managementProjectVersioning04.png)

We can then push those changes back up to the MS-Common-Library solution version:![Image](/media/blog/microservices-source-code-managementProjectVersioning05.png)

And at some later point, the MS-Projects development team finally decide to update.  They can then of course choose to pull the latest version:![Image](/media/blog/microservices-source-code-managementProjectVersioning06.png)

At which point everyone is up to date.
For three projects, this may seem very complex.  When you get into an enterprise environment with tens or hundreds of projects, it allows you to make changes to specific projects without having to change every project (thus making the change easier to developer, test &amp; release).
### How to achieve
Now for the difficult bit - how we can use source control to successfully manage this.
I want to use GitHub so that I can share my work.  So we look at the options in git.  I'm not going to pretend that I'm an expert in git - and I'd suggest you read these two articles before going much further:
* [Mastering Git submodules](https://medium.com/@porteneuve/mastering-git-submodules-34c65e940407)
* [Mastering Git subtrees](https://medium.com/@porteneuve/mastering-git-subtrees-943d29a798ec)

From the articles (and a fair amount of experimentation) I settled on submodules.  The articles do warn about some of the submodules short comings (and there are many articles on the web being less than kind about them) - however they seem to meet my requirements quite well.
(Experience may provide me wrong - I'll update this page if it does).
You will notice in my GitHub account I now have three projects (probably more by the time you read it) one for each of the solutions.  (Note that I've called them all MS- something to group all my Microservice projects in GitHub).
(Additional note; I've realised during review that I've reference MS-Project above and given examples for MS-Logger below.  Both are Microservices I plan to build - I've just managed to forget which one I was planning for the article - that's what I get for writing half one day and half the next.  The MS-Logger solution has the same structure as the MS-Project solution referenced above).
I first created three almost empty repositories on GitHub (just had an auto created README.md).  I then cloned all three projects to my development PC.
![Image](/media/blog/microservices-source-code-managementTopLevelFolderStructure.png)

I've then created my (generally empty) solutions into their relevant folder.  The below is an example for the MS-Logger solution (File -> New Project):
![Image](/media/blog/microservices-source-code-managementNewProjectMS-Logger.png)

Note that I choose to add to Source Control but deselect the "Create directory for solution" (we've already done this by cloning the folder from GitHub).

At this stage I save the solution and use Visual Studio commit the changes, then sync the project to GitHub:
![Image](/media/blog/microservices-source-code-managementMSLoggerGitHubInitial.png)

Ok, so I now have a default solution for my MS-Logger.  As per the above, I also need MS-Common-Library and MS-Base-WebAPI adding to my MS-Logger solution.

For this, drop out to git (I use via PowerShell), and within the MS-Logger solution folder I create a folder called remote - I use this purely as a container for any submodules.  Helps to remind me in my old age what type of project they are.  I then issue the following:

<span style="background-color: #eeeeee; font-family: Courier New, Courier, monospace;"><span style="font-size: xx-small;">git submodule add https://github.com/Red-Folder/MS-Common-Library.git remote/MS-Common-Library
git submodule add https://github.com/Red-Folder/MS-Base-WebAPI.git remote/MS-Base-WebAPI</span>
</span>This will clone the projects under the MS-Logger solution.  If you browse to the remote folder and then the subsequent sub project, you will see that all of the project contents have been cloned - it looks like it has become part of the MS-Logger repo - this however isn't true.

The folders (MS-Common-Library &amp; MS-Base-WebAPI) are actually separate git repos to the parent MS-Logger (as per the submodule article above).

From Visual Studio, add the MS-Common-Library &amp; MS-Base-WebAPI existing projects to the solution.  Commit the changes and sync.

Note that if for any reason the remote folders/ projects aren't committed to the GitHub, you may need to do manually:

<span style="background-color: #eeeeee; font-family: Courier New, Courier, monospace; font-size: xx-small;">git add *</span>
<span style="background-color: #eeeeee; font-family: Courier New, Courier, monospace; font-size: xx-small;">git commit -m "Added remote projects"</span>
<span style="background-color: #eeeeee; font-family: Courier New, Courier, monospace; font-size: xx-small;">git push origin master</span>

Within GitHub you should be able to see how the submodules are represented:

![Image](/media/blog/microservices-source-code-managementGitHubRemotes.png)

You will see that the submodules are reference by commit SHA.  This allows our MS-Logger project to reference any MS-Common-Library/ MS-Base-WebAPI commit.  This permits us to choose when we clone new versions into our projects (using the <span style="font-family: Courier New, Courier, monospace; font-size: xx-small;">git submodule update --init --recursive</span> command).
The added benefit of this approach is that it allows us to automated the version checking.  Which leads us to the next post.
### To be aware of
One word of warning; if you make changes to the sub-project, then make sure that the changes are committed and synced with your central repository.  Otherwise your parent project will reference updates that are only on your development PC (see the above submodule article for a much better explanation).
If you are using Continuous Integration to check out from the central repository to test &amp; build; then this problem should be caught by this.
### Summary
We have now setup the basic source control structure (using git submodule).
In the next article I will produce a Microservice that checks for inconsistencies in submodule versioning.
