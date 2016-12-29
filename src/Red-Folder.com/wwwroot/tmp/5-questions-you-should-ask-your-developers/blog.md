In this article I give you 5 questions I feel that you should be asking your development team.

During this article I use a Word document as an analogy for a software project. I wanted to keep the technobabble to a minimum and a Word document should be a fairly universal concept to all. It is a huge over simplification, but the principals hold true.

## Do you have a copy of the document from 3 months ago?
Your development team will be working on one or many Word documents over a prolonged period. This could be weeks, month, years and in some cases even decades.

So image multiple developers making changes to your Word document over the course of twelve months. They add a paragraph or fifty, they reorder the chapters, change some titles, fix a whole bunch of spelling and grammatical errors and remove extraneous content.

You later come to the document and find that someone has removed the all-important price list – possibly the most important part of the document (for you anyway). You remember it being there two months ago when you last needed it – but now it’s gone.

The price list was months of painstaking work from all involved and something you’d dearly not want to start again from scratch – plus you've just promised it to you biggest customer.

This is where hopefully your development team say “no problem, we’ll get the old version out of source control”.

Think of Source Control as a curator and guardian of every different version of your Work documents.

Let’s imagine Betty (I'm sure why I've chosen that name – I'm sure that it says something deeply psychological about me that I’d rather not think about). Her job is to take a copy of every version of the document as it is changed. Each developer will go to Betty at the end of the day (or as needed) with their updated document(s), Betty will then file that change in her collection of filing cabinets along with a note of which developer passed it to her. If Betty is really good at her job (which she is) she will also insist that the developer tell her why the change was made.

At any time you can ask Betty for the last version with the price list, who removed it and, possibly more importantly, why.

There are many products available for source control – each have their own merits. The decision on which should be left to the development team. But they should be able to demonstrate an ability to get you old versions - along with who made the change and why.

## How much is our technical debt?
> "Technical debt (also known as design debt or code debt) is a recent metaphor referring to the eventual consequences of any system design, software architecture or software development within a codebase." Wikipedia

You know the finances of the company. You understand if the company is in financial debt and you have a plan in place to service this debt. Debt isn't a bad thing if it is understood and in control. If you don’t understand your debt or have a means of servicing it – then bad things will happen.

Technical debt is exactly the same principal. It is acquired when something in our Word document is fine for now, but will cost us time and effort to resolve in the future. For example, the development team have pulled an all-nighter to add the changes you wanted to document – and while everything is in there, the document hardly flows the way it used to. But it does the job so you send it out to your biggest customers and you forget about it. Time passes and you repeat the process – and in the back of your mind you are thinking that the document is hardly as polished as it used to be. Eventually you get you customers coming back to you complaining that it’s almost impossible to read.

Rather than having dealt with the problem when it was small, you've now let it grow to the point that fixing it will take weeks of painstaking work by the development team – this is you technical debt.

Sometimes it is correct to allow technical debt to achieve an aim (in the same way you would accept financial debt for the improvement of the business). Sometimes it happens by accident. But the important thing is understanding how much you have.

You can then make decisions on when to pay that debt down – rather than having the technical equivalent of the bailiffs turning up at the door.

Technical debt can be difficult to measure. The best method is to establish a culture where technical debt is understood and tracked. As developers find (or create) technical debt, keep a track of it and an estimate of how much work it is to resolve. You then have a choice on when to service it.

## How are they keeping up with technology changes?
With changes to development tools, processes and principles occurring daily – this is a great question to ask.

I've seen various articles describing the half-life of developers skills being between 2-5 years – roughly meaning that half the skills they know and use today will obsolete within that timeframe.

Thus developers (and everyone in my opinion) need to be constantly learning new skills and improving old ones.

One of my first bosses said “If you haven’t learnt something new today, then it was a waste of a day”.

I firmly believe that continual learning development should be baked in with every techie. So much so I've added the following to every interview I conduct:

* How do you keep current with technology?
* What one skill would you like to master this year?

## But what does this mean to your business?

Software development is a relatively young industry (circa 70 years old) and is still very much experimenting and growing. All of that leads to better ways of performing software development – be that faster, with more quality, with more accuracy, or with more efficiency.

Do you want your development team spending 3 months writing something, when an update to Word could have the job done in a week?

## How do we limit the cost of mistakes?
Mistakes happen.

The important thing with development is to catch them as early as possible.

Take for example if a developer makes a spelling mistake in our document.

If the developer spots it straight away then great, it gets fixed and costs are negligible.

Now imagine that it didn't get spotted. The document is sent to the printers for a 10k print run. It’s handed out to your entire customer based at your yearly conference. It even appears on the slide pack behind you are you are delivering your keynote speech.

How much does that cost you?

This is why you would use an automated spell checker. This is why you would use a proof reader. The same should be true for your development team.

Within development, the equivalent of proof reading is peer review and manual testing.

Peer review is great for checking that firstly the change does as requested and secondly that it was a good way to do it. This latter point comes back to the technical debt point raised earlier. Sometimes there are many ways to solve a problem – some will be more maintainable over the life of the software than others. This should always be a collaborative decision within the development team – it helps them to grow and learn from each other.

Manual testing is dull. Spending hours looking at the same screen, making sure that everything works as expected (not just what we have changes, but also everything else in case we accidently broke that) – it’s almost the entire opposite of a developers natural disposition to be problem solving. It is however essential to test thoroughly – without it then you can have no confidence that you are releasing very costly mistakes into your business. Some businesses employ a separate quality team for testing (personally I prefer it to be part of the development team’s responsibility).

Some testing (note, not all) can be automated. There are a growing number of tools to help (a bit like our spell checker in Word). The developers can use those tools to build automated tests that can produce a huge amount of validation very quickly.

What would you prefer? Your development team spending two days manually testing or it being done automatically while they move onto the next big thing?

It should be noted that automated testing doesn't come for free. While there can be costs with some of the tools, the tests themselves have to be written by the development team. This will take time and should be considered on a cost vs benefit basis. There is a huge library of work written on approaches to building automated testing. For me the general advice is test the critical and what is heavily changed – then evaluate to see if there is benefit in doing more (generally there will be).

Referencing the price list problem above; wouldn't it have been great to have an automated test to tell you that it had been removed?

## What do they need to be better at their job?
The most important question by far.

Your development team will invariably be a group of very intelligent people. They will be natural problem solvers and will always be looking for ways to improve themselves, their work and the processes they work within.

They will generally need help to achieve do it - be that time, money or even a simple nod of approval.

I can do nothing else here but recommend that you build a culture of open conversation – one in which everyone is encouraged to contribute. And between the occasional requests for a slide in the office (I blame Google) there will be some real gems.