In this article, part of my series explaining better ROI from software development, I continue the story of Fiona and team removing dependencies.

## Town hall meeting
Following the team workshops (see [Part 1](/blog/roi-of-dependencies-part-1)), Fiona’s team held a town hall meeting.  They opened the meeting with a summary of their thoughts to date, then invited questions.

## Different Directions
One of the DBAs asked how deviation between the squads could be avoided.  The organisation had previously had a problem with their databases – a problem that the DBA team has spent considerable effort to resolve.  He was concerned that with different teams having different focus, the deviation was inevitable.

The team fielded this by referencing guilds at Spotify.  Within Spotify, they operated guilds across the squad boundaries based on subject matter (DBA for example).

![Guilds](/media/blog/roi-of-dependencies/Guilds.png)

Doing this would allow subject matter experts to share best practice, training, etc.

Day-to-day, individuals would be focused on delivery.  But with regular checkpoints, they could ensure that any deviation was a conscious action.

The team also suggested that by being closer to delivery, an individual could choose to experiment outside of the “norm” and feedback into the wider Guild – thus making the overall practice stronger. 

## Line Management
A HR assistant then raised the question of line management.

Fiona fielded this.  She confirmed that line management wouldn’t change – line management was important for individual development and support.

The line manager wouldn’t however be responsible for the day-to-day workloads – that would be handled locally within the squad.

She stressed that this was to be open to review – as was everything over time – but it would be similar to matrix management (see [Matrix Management]( https://en.wikipedia.org/wiki/Matrix_management))

She expected the line management to follow similar lines to the Guilds.

## How do I support my business?
A member of the IT helpdesk then asked how they would be able to get support for the business.

The team suggested that the support would actually be easier (and better) going forwards.  As the squads would be product based, it was much easier to identify which squad owned the problem – rather than trying establish if it was software, hardware, networking, database, etc – to then route to the appropriate team.

It was expected that an individual from each squad would be rota’ed as the support contact.  It was felt that was the fairest method to ensure that all squad members understood the product and that any support workload was shared.

Fiona also added that they believed that a squad responsible for support would be motivated to provide the best quality software possible.  She referenced an [article]( http://queue.acm.org/detail.cfm?id=1142065) in which Werner Vogels, CTO of Amazon, said “You build it, you run it”.

## 24x7 Support
The member of the helpdesk followed up their question with how that related to 24x7 support.

Fiona commented that round the clock support was unfortunately a by-product of a successful business and that the squads would be expected to provide it.

She felt that, like the support, it should be rota’ed through the squad members to share the burden.  She said that while this was likely to be new to some staff, it was fairer on those staff members who had been traditionally been more support focused (who bore the brunt of the out-of-hours) – that the burden would now be shared across the entire squad.

She committed to back any squad that felt they needed to make quality fixes to reduce the occurrences of those out-of-hour calls.  She confessed that she hoped a by-product of the policy to be better quality.  While she knew no one put out buggy code on purpose – she was fully aware from her own career how much the prospect of support at 2pm in the morning influenced quality.

She also committed to looking at the remuneration for being on call – that that was only fair.  Also that 1-2-1 conversations would be had to address any personal concerns this raised.  Where possible the organisation would attempt to resolve any problems – but ultimately the responsibility of the smooth running of the system rested on the shoulder of the relevant squad.

## Core Infrastructure
A network engineer then raised a question over core infrastructure – such as networks, virtual machines hosts, etc – the core that all other systems relied on.

Fiona admitted that this was something that needs some thought as the trial progressed.  It was however the current thinking the “core” would be just considered another system – with a relevant squad attached to it.

The squad would likely to be more “infrastructure” heavy that other squads due to its responsibilities, but the squad principles all appeared to hold true.

She also thought it was good to have some amount of development resource in that squad to help provide automation services – providing a level of self-provisioning.

Again she admitted that this wasn’t fully formed and would expect it, like the entire principles, to adapt and evolve as they all learnt how to best work.

## More staff
A server engineer then asked if additional staff would be required.  He could see more squads than server engineers for example.

Fiona said that this was something that be reviewed on a squad by squad basis.

The product(s) being managed by the squad would dictate the skills that they required.  She reminded her audience that she wanted a squad to contain all the skills necessary to take a change from idea to realisation.

That wouldn’t necessarily mean that every squad would need a server engineer.  Just enough of the skills to do what was needed.

That may come down to training squad members in new skills.  It may involve further recruitment.  As she said it would be on a case by case basis.

She was however really excited by the idea of the “core” system squad in that, specifically for infrastructure, they could really be critical in enabling other squads by providing that level of self-provisioning.  Thus empowering other squads to be more self-sufficient.

She admitted that there was always going to be times where a specific skills wasn’t within a squad – especially in situations where it needed to be used for a very short period.  In those instances, she expected the squads to help each other as appropriate – may be through the guild structure.  This was something she was hoping that they could handle in their stride.

## Giving breathing room
Fiona called the Town Hall meeting to a close.

She thanked everyone for their input.  Based on the level of interest, she had decided that she would run a second town hall in a weeks’ time.

She welcomed any input or further questions and that she was looking forward to the next meeting.
