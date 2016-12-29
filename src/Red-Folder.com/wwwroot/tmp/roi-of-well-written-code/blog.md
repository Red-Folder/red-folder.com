In this article, part of my series explaining better ROI from software development, I’d like to look at well written code.

## Why does it matter?

Software Development produce programs using one or a combination of programming languages (or code).

Looking at one of these you will see a lot of recognisable words - regardless of if you have any programming experience.
![Code](/media/blog/roi-of-well-written-code/Code.PNG)

Why? Because we are humans, and writing in a human language is so much easier for us.

The computer however would prefer 1 & 0 (ones and zeroes) – because that is what it understands.

So when we program, we write code for humans (even if we don’t realise it).

Yes that code will be turned into something that the computer understands (a process called compiling) – but the written code is first and foremost for humans.

You may well still be thinking why? And I can understand that.

It seems counter intuitive that we are writing instructions for the computer, but producing in a format that favours our fellow humans rather than the computer.

In practical terms, it’s simple enough.

While it is possible to write instructions directly for a computer – it is an extremely impractical long winded task for a human brain to accomplish.

It is however much more practical to produce a program which translates from our programming languages to instructions understood by the computer. And over the years we have developed abstractions which make it easier for developers to produce those instructions.

So if this is the case, who is it we are writing the code for?

Yes the code will be to solve a problem - to ask the computer to perform actions. There will however be multiple methods of achieving the same result.

## Getting from A to B

Imaging that you are writing instructions on how someone can travel between two points in a city.

Unless it is a trivial route, you could reasonably expect as many different instructions as people you ask. Some will pick different routes – maybe based on speed or personal preference, some will pick different means of transport, and some will provide incorrect instructions.

Now if you redistribute those instructions to each other and ask them to be critiqued, then you’ll likely to get a lot of “I wouldn't go that way” or “they missed a step” or similar – and in some case incorrectly because they didn't understand the intent of the original author.

It may be the case that one author provide walking instructions which take you through a park as they felt that it would quicker than taking a taxi or bus.

Another may provide you a bus route that seems indirect – but they are aware of major roadwork which their route avoids.

Now imagine that same exercise which instructions why a particular decision was made. Reviewers will now have an understanding of the intention behind the solution. They may still disagree, but at least they have the relevant details to make that choice.

And the same is true in code.

A developer has solved a problem. But without a clear idea of the intent, it can be unclear to any future reviewer (including the developer themselves) why the problem was solved in that manner.

And it is this level of ambiguity which can cause additional time and effort trying to divine the original intent – thus effecting productivity – thus impacting ROI.

## What is well written code?

Think about a legal contract.

I'm sure you will have come across a number previously that are so difficult to interpret that you end up spending hours poring over it with a pen and highlighter getting to the true intention.

You end up with a document so complicated that the only want to truly validate it is to take it before a court.

This is why you have movements such as the Plain English Campaign:

> “Since 1979, we have been campaigning against gobbledygook, jargon and misleading public information. We have helped many government departments and other official organisations with their documents, reports and publications. We believe that everyone should have access to clear and concise information.” [Plain English Campaign](http://www.plainenglish.co.uk/about-us.html)

Well Written Code is much the same. It is code that is clear and concise in what it is attempting to do. It should provide enough information to express the intent quickly.

## Does documentation help?

At this point, I'm generally asked about documentation.

For me, documentation isn't the silver bullet. If anything it can be misleading.

The key problem for documentation is that it can so quickly become out of sync with the software code. I've yet to see a reliable method of keeping the two in sync – thus your documentation may say one thing and the code something different. From an ROI perspective you've just greatly reduced productivity because of the effort to produce the documentation and the confusion caused when it doesn't match the code.

There is however some value documentation. To provide a high level overview of the system.

But it needs to be kept brief and visible. The minute it deviates from the code it needs to be updated or destroyed.

My personal preference is a wall/ shared space with some high level diagrams that everyone (and I mean everyone) can understand. These diagrams should be a constant reference when changes are being made. Without them being a living artefact, they quickly because the problem I describe above and you are better off without them.

The true source of what the code is attempting to achieve is the code itself.

## Practical options

Code should be well written. Think about the Pain English Campaign I mentioned above – a developer shouldn't need to spend a lot of time trying to interpret the code. If they are, then it will cause an obvious negative impact on ROI.

How to write good code however is a long (and technical) subject to which many books have been devoted. Thus it won’t come as a surprise to you that your development team will need to learn over time what constitutes good code.

This is where you need to provide the resources and time to allow your team to develop a shared understanding of quality AND the time to achieve it.

This will mean that the team need the time to not just solve the problem set them, but also then make it clear to future developers.

This is no different than in writing. You can write an essay which conveys your meaning. Art then takes a part in refining that essay down to the core of what you need to convey. The shorter and more concise you make it, the easier & quicker to understand.

> “I have only made this letter longer because I have not had the time to make it shorter." Blaise Pascal, The Provincial Letters

> “It is my ambition to say in ten sentences what others say in a whole book.” Friedrich Nietzsche

Within software development this practice is called Refactoring.

## Refactoring

> “Code refactoring is the process of restructuring existing computer code—changing the factoring—without changing its external behavior. Refactoring improves nonfunctional attributes of the software.” [Wikipedia](https://en.wikipedia.org/wiki/Code_refactoring)

So for us, we want the refactoring to continue to solve the problem we have solved – but make it more productive for future developers.

From a process perspective, this will normally involve a developer revisiting the code a number of times to refine their solution.

There is a danger this can lead to gold plating. I would however see the risk of this being considerably less that the overhead of having poor productivity stemming from poor code in the future.

## Summary on ROI

The less time a developer takes to understand code, the quicker they can change it.

The clearer the intent of the original developer in that code, the less likely that the changer will inadvertently break the system.

While this may require more work up front, it will be so much more cost effective for future changes. Thus a positive ROI.