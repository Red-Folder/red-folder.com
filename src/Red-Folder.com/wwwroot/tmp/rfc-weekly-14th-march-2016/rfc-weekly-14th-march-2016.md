## Development
### Learn X in Y minutes
Useful little [helper site](https://learnxinyminutes.com/) providing cheat sheets for getting a quick start on a program language.

### Building Microservices with Event Sourcing and CQRS
This [presentation](http://www.infoq.com/presentations/microservices-event-sourcing-cqrs) talks about Event Sourcing and CQRS within the Microservice world.

Both Event Sourcing and CQRS are quite popular within software architecture.  They build on DDD principles (although can be used separately from it).

Event Sourcing looks at retaining data in an event stream rather than a snapshot of time that we are familiar with in RDMS.  This about it as a list of all the transactions (credits and debits) that have been made against your bank account rather than a current balance.  The events allow you to have a full audit where as the current balance is just the current snapshot in time.

The current balance can be calculated by totalling (sum in this case) of the transactions.

To avoid the performance overhead of a walk through the event stack for the current state, you can implement event listeners which would produce a snapshot of the current state (a read only version that you may have had in the RDMS).  You can register multiple listeners so that you could generate multiple optimised views of the event stream - think of it like fact tables in data warehouses.

Event Streams aren't appropriate for all problems - but are great for being able to see the version at any point.  Look at source control systems for an example.

CQRS is Command/ Query Responsibility Separation.  This talks about separating the logic of command (insert, update, delete) from query (select).  By separating the two distinct activities you can make services more focused (easier to dev &amp; test) and allow scaling independently.

## Development Process
### Agile Productivity: Willpower and the Neuroscience approach
This great [article](http://www.infoq.com/articles/agile-productivity-willpower-neuroscience) talks about agile productivity and how it is influenced by willpower - or more importantly by avoiding the need to rely on it.  It talks about the neuroscience behind willpower and techniques of how to use that willpower effectively in relation to performance.

Good news here is that a lot Scrum practices play to those techniques.

### Scrum with Trello
I love Trello.  This [article](http://www.infoq.com/articles/scrum-trello) isn't ground breaking - but most of the suggestions are ones I've used before - so useful if you've not played with Trello before.

### It's Just a White Board
In this [presentation](http://www.infoq.com/presentations/kanban-why), Jim Benson discusses why Kanban is helpful.  While he does give an overview of Kanban, he also talks about the physiological positive effects of physically moving a sticky to done.

If you're following my ROI series on LinkedIn, then you will see the tie into the promoting the Intrinsic Motivators.

## Infrastructure
### Azure/ AWS Mapping
Useful [article](https://azure.microsoft.com/en-us/campaigns/azure-vs-aws/mapping/) here to compare between Azure &amp; AWS.

## Self Promotion
In the latest article of my LinkedIn series on better ROI from software development, I have released part 2 on Agile Software Development.

You can read there article [here](/blog/what-is-agile-software-development-part-2).

## And finally ...
This [website](http://whichcatisyourjavascriptframework.com/) shows you what type of cat your JavaScript framework is.

Probably the most important thing you'll learn this week.
