## Octokit.net & creating a commit with deletes
I've been using Octokit.net again recently - all part of my blog site redevelopment (more on that hopefully in the coming weeks - yes, I know I'm always saying that).

Yet again, I think I'm finding the edges of the Octokit.net implementation.  Not sure how, but I seem to always look at non-mainstream stuff.

To cut a long story short, to create commit with multiple deletes you need to go through a reasonably complex proceess.  And while Octokit.net can be made to perform that process, it seems to be missing an obvious piece of linking logic.

I've raised a [Github issue](https://github.com/octokit/octokit.net/issues/1610) with the team to see if I'm getting the wrong direction - but I now have functional code that works.

While I may blog separately on this, in short the code is as follows:

<code>
// Setup tasks
var credentials = new Octokit.Credentials(_username, _key);
var connection = new Octokit.Connection(new Octokit.ProductHeaderValue("Red-Folder.Playground"))
{
    Credentials = credentials
};
var client = new Octokit.GitHubClient(connection);
var parent = await client.Git.Reference.Get(_username, _repo, "heads/master");
var latestCommit = await client.Git.Commit.Get(_username, _repo, parent.Object.Sha);
var currentTree = await client.Git.Tree.GetRecursive(_username, _repo, latestCommit.Tree.Sha);

// Create a new tree from the current tree
var nt = new NewTree();
currentTree.Tree
            .Where(x => x.Type != TreeType.Tree)
            .Select(x => new NewTreeItem
            {
                Path = x.Path,
                Mode = x.Mode,
                Type = x.Type,
                Sha = x.Sha
            })
            .ToList()
            .ForEach(x => nt.Tree.Add(x));

// Remove a file
var toRemove = nt.Tree.Where(x => x.Path.Equals("2017-01-29/TestFile.txt")).First();
nt.Tree.Remove(toRemove);

// Submit the tree, commit and point master at it
var newTree = await client.Git.Tree.Create(_username, _repo, nt);
var newCommit = new NewCommit($"Testing commit", newTree.Sha, parent.Object.Sha);
var commit = await client.Git.Commit.Create(_username, _repo, newCommit);
await client.Git.Reference.Update(_username, _repo, "heads/master", new ReferenceUpdate(commit.Sha));
</code>

The important piece is creating a clean Git tree from the current tree (plus removing all references to that current tree).  All seems to work - but waiting to see if anyone comes back with feedback.

Credit where credit is due, most of my logic comes from this [great post](http://www.levibotelho.com/development/commit-a-file-with-the-github-api) - which actually talks about the raw Github API - but easy enough to convert.

## Software as a Service (SaaS) database/ Azure patterns
Useful [article](https://azure.microsoft.com/en-us/blog/saas-patterns-accelerate-saas-application-development-on-sql-database/) from Microsoft providing a:

> SaaS application and a series of management scripts and tutorials that demonstrate a range of SaaS-focused design and management patterns that can accelerate SaaS application development on SQL Database. These patterns extend the benefits of SQL Database, making it the most effective and easy-to-manage data platform for a wide range of data-intensive multi-tenant SaaS applications.

I've not gone through in detail ... but always good to get sample application from Microsoft.

## Contract Process
My current contract is drawing to an end (ends 30th June).  As I talked about in this [previous blog](/blog/rfc-weekly-23rd-January-2017), I'd generally start my process from about 4 weeks out.

So, as per last time, while expecations are high for a renewal, it would be foolhardy to rely on it.  Plus of course a great time to update my CV, Website, etc.

So this week, I'll updating my offline CV, LinkedIn, and [Website BIO](/MyBio).

I'll probably leave it until I'm two weeks out before pushing my CV out of my network and the jobs boards.

I've also got my financial Year End this month, so I'll need to putting some planning in for that as well.

## Shameless self-promotion
No new ROI articles for the last two weeks.  I've been busy looking at some reports regarding ROI of DevOps.  I'm probably a distance of turning those into articles, but found some really complelling business cases.

Following an estimation fail by myself on Friday night, I'm likely to blog this week about the ROI of Estimates - time allowing.
