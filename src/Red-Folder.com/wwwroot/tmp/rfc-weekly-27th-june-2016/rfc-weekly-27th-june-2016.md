## [Why Agile is still relevant?](https://www.infoq.com/news/2016/06/scott-staples-agile-relevant)
I see a lot of bashing of Agile.  There seems to be a bit of backlash against Agile within the industry – largely I believe that it was sold as this miracle fix that cost little but benefited hugely.

Now for me I’ve always believed in the mantra of easy to understand, difficult to master.  And I think that is where a lot of people fall down.  They do part of Agile (Scrum stand ups for example) then are turned off by the lack of instant success.

A lot of this is about training and understand firstly the difference between Agile as a mindset, agile frameworks such as Scrum and practices that help you achieve agility such as TDD or Continuous Integration/ Delivery.  And secondly the organisational buy in to adopt it all.

The above article is great and takes a look at some of those organisational changes.

## [Cyber Dojo](http://www.cyber-dojo.org/)
> "Dojo is a Japanese term which literally means "place of the way". [...]  In the Western World, the term dōjō primarily refers to a training place specifically for Japanese martial arts such as aikido, judo, karate, or samurai" [Wikipedia](https://en.wikipedia.org/wiki/Dojo)

In this case the Cyber Dojo is online training facility for group development learning.

You first job is to create a session – choosing programming language, testing framework and scenario.

You then make that session available for your teams to practice on.

The principle is based on gamification.  Each is trying to do the best they you can within your peer group.

Ideally everyone does at the same time for a period, then reviews how everyone got on.

This should be a learning experience in which we review how each individual (or pair) approached the problem, developed the code and the tests.

Because of the nature of the scenarios being fairly short, this makes a great platform for code camps or bringing a team on.  I’d also suspect interesting with more experienced developers as an aid to bonding &amp; code review.

## [Service Worker](https://developer.mozilla.org/en-US/docs/Web/API/Service_Worker_API)
Service Worker provides a level of abstraction between the browser and the network.  As I understand it, you provide a JavaScript proxy that handles the network requests from the browser and it can choose how to handle those requests.

For example, it could return canned responses if the network is unavailable – or maybe queue up requests for processing when the network comes back online.

It should allow you take a fair amount of network logic out of the UI thread as it runs in a worker thread.

At this point, is seems to be considered experimental.  According to CanIUse.com, it doesn’t appear to be supported by IE or Edge at this stage.

### [Visual Mutator](http://visualmutator.github.io/web/)
<blockquote class="tr_bq">“VisualMutator is integrated into development process as an extension for the Visual Studio IDE. Implementing the process of mutation testing, it strives to provide a way to measure the quality of the test suite.” http://visualmutator.github.io/</blockquote>What this means in English is that it randomly changes your source code, runs it through your unit tests and then shouts at you if you test suite passed clean.

The idea is that this should give you some confidence (or not) in that your test suite is correctly protecting your code base.

Think of it like [Netflix Simian Army](http://techblog.netflix.com/2011/07/netflix-simian-army.html) for your codebase.  How fun is that.

### [Datadog](https://www.datadoghq.com/)
A SaaS solution for monitoring your cloud infrastructure.  Its setup to take metrics and logs from a variety of cloud platforms and services.

Not a tool I’ve had a chance to play with yet, but one for the list.

### Shameless self-promotion
No articles last week.  My accounts took centre stage (and way too much of my time).

I should have one out on Wednesday/ Thursday on LinkedIn looking at implementing Agile.  The idea is to start to introduce frameworks like Scrum and practices such as Continuous Integration.  Again these will be from an executive ROI perspective rather than a technical.

I’m then on leave for 2 weeks – during which I promised myself to keep away from the keyboard.  So if all goes well then you should get radio silence for two weeks.
