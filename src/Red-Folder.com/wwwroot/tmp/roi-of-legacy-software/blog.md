In this article, part of my series explaining better ROI from software development, I’d like to look at legacy software.

## TL;DR

Legacy software generally has many negative connotations around it. It is very common to see them as an opportunity to rewrite from scratch (greenfield). In this article, I look at why this may not be the best approach from an ROI perspective.

## What is legacy?

Legacy can be used as quite a negative term around software that isn’t well liked. It isn’t uncommon for the term to be used with any software that has fallen out of favour with the teams support or using it. Maybe it isn’t the latest technology, maybe it isn’t well understood, maybe it just works and nobody goes near it.

Wikipedia describes it as:

> “In computing, a legacy system is an old method, technology, computer system, or application program, "of, relating to, or being a previous or outdated computer system." Often a pejorative term, referencing a system as "legacy" often implies that the system is out of date or in need of replacement.” [Wikipedia](http://en.wikipedia.org/wiki/Legacy_system)

There are however many different definitions in use within the software industry – including the view that everything created today, is legacy tomorrow.

For me, legacy can be used as term for any software that causes you not to sleep at night.

## Signs you software is legacy?

If any of the following are true, then I’d suggest that your software falls into that legacy definition:

* The software hasn’t changed in over 6 months
* Nobody can change the software
* You are struggling to employee people with the appropriate technical skills
* Your development team don’t enjoy working on it
* Your development team are spending more time keeping it going that innovating 
* Any change takes months to deliver
* There are considerable manual processes involved with the software
* You generally make a small prayer before using it

This list isn’t complete – but should give you an idea of where I’m coming from.

## Are you going to sell me a new system?

I can if you really want. But that isn’t my default stance.

While even the Wikipedia quote above talks about replacement, I generally believe in evolution over revolution.

I generally would advocate improving the system over writing it again.

## Why not just rewrite?

Sometimes a rewrite is the correct answer.

I’d ask you to consider how well you understand the system, how much investment has gone into it and what are the actual problems with it.

## Understanding

I generally find that understand why a system was built the way it was is lost within the mists of time. At some point a conversation has been had between the developer and someone in the business and a new feature has been born.

I’d wager that 3 months later you’d struggle to get either party to write down how it was intended to work – yet now your business relies on it.

Yes that logic is baked into the software – but that is very difficult to convey into a specification that can be passed to a new development team. It can be re-engineered, but it is subject to a lot of discrepancy.

## Investment

Over time most systems will have had considerable investment put into them. The cost of the development and maintenance. The cost of licences. The cost of training. Etc

To then re-invest considerable sums in re-writing it to achieve the same (but nicer and shiner) system? Does that make sense?

## The actual problem

If you’ve got to this point in the article, I would assume that you have a system in mind and a problem associated with it.

More often than not, the problem can be solved without a replacement.

Again, I would reiterate that sometimes it does make sense – but probably less so than the number of replacement projects in flight around the globe.

## So why evolve it?

As I’ve said, I often find people jump too quickly into replacing it. As a general rule however you have two real problems with that;

* You will either have to “freeze” change to the original system or have to double the effort of any change
* It can be months for even the most trivial system to be replaced

The first of those should be easy enough to review in your own environment. If the original system hasn’t changed in a long period (at least as long as it takes to replace it) then there is no impact. If there has been limited change, I’d probably question your reason for replacing it in the first place. If however there is regular change, then you have a hard problem to resolve – you either have to implement a “change freeze” until the new system is available (very rarely palatable or practical) or you have to duplicate the effort on original and new systems (doubling the cost).

The second, the length of time, not only magnifies the effects of freeze/ duplication, it can also give you very delayed feedback. By virtue of everyone understanding the work to be “copy the original – but make it better” – it is very easy to leave a development team to crack on with it. Without regular inspection, you can come back months later to find that it really isn’t doing what is expected – especially if you need to get it into live use to prove it.

Both of these can have significant financial impacts.

Whereas, evolution handles these problems;

Any changes are blended into any evolution activity. And if you work in small changes, release, validate cycles you can be confident that the system is both still correct AND heading in the right direction.

The question then changes to how much you want to invest in that evolution activity. And with the right team in place you made that decision week to week, month to month.

## Ok, so is there a practical checklist?

Every situation is different. To really be effective a bespoke plan is likely to be required (and evolved along with the work).

A general checklist however I’d suggest:

## 1. Source control

Source control is a means by which you have versions of the software. This provides a safety net that (if used correctly) meaning that you can experiment. If the experiment fails, then reverse back to a “known” good point.

Without source control, it can be almost impossible for a development team to reverse out a failed experiment – more often in leads to problem after problem being added to resolve. I’ve seen projects spiral out of control when this happens.

## 2. Test Environments

If you are going to change a system, then you need to be confident that you can validate BEFORE you put it into live.

I’ve seen too many companies skip this with the excuse that it is too expensive to produce a test environment.

But rarely do they measure that cost against the risk & impact to your live environment. There has to be a balanced view taken.

## 3. Seams

Find the “seams” in the systems.

Most systems will be made of logical parts or components. The more you can identify these and split them apart at the seams, the more focused and lower risk changes can be.

General rule of thumb – the smaller the change, the smaller the impact, the safer, the cheaper and more cost effective.

## 4. Tests

Test, test and test again.

If you are working on small changes, then testing between each change should quickly highlight where the problem occurred.

Again, general rule of thumb, the further (in time and people) a bug gets from the developer that coded it, the more expensive it is to resolve.

## 5. Release

Put the change live. May seem obvious, but without putting the change live, how will you know if works.

Avoid batching changes. Keep them neat and separate. It will make fault finding, training, etc much easier and cost effective.

## 6. Feedback

Review the output of the testing.

Review the output of putting it live.

This isn’t about finding and attributing blame (there is rarely any positive ROI out of that).

It is a recognition of “we are where we are” – what do we do next. If you’ve been reading any of the Agile stuff, then this will be familiar to you.

With regular feedback, then you are in great position to make investment decisions. Is this working? Have we done enough? What is the most important thing to do next?

Obvious control over your investment is paramount to ensuring the best ROI. Too many projects are assigned a “budget” with little review if it actually delivering value.

## 7. Repeat

Keep repeating steps 3 to 7 until you’ve decided that you don’t want to invest more – be that because it is proving to be too expensive to evolve or because you have achieved what you set out to do.

## My ode to legacy

I really do enjoy working with legacy code.

That isn’t common across the development community. There are those that prefer an entirely new shiny problem and would advocate a rebuild every time.

But I know that I’m not alone in enjoying legacy code. There are plenty of us out there that value an existing system.

Part of it is a sense of almost archaeology as we delve into a system. I can’t help but compare myself to Indiana Jones trying to work his way through a long dead civilisation. And yes, occasionally I do set off the odd giant stone boulder
![Indiana Jones](/media/blog/roi-of-legacy-software/indianajones.jpg)

Part of it is learning how others have solved problems in the past. Learning from that and then trying to do better. Apart from the technical game of one-upmanship – it also gives pretty immediate results. I can be delivering value to the business in a matter of days and week rather than weeks and years.

One small takeaway; it is important to consider the developers when you are looking at how to take a system forward. Some will love working on legacy, some will hate it, others will prefer a blend between greenfield & brownfield work. Understanding the team really can make a difference success.

## Shameless self-promotion

With over 20 years of software development experience, Red Folder Consultancy can help you with your legacy software problems.

Well versed in being parachuted into complex situations, RFC can be contacted at www.red-folder.com