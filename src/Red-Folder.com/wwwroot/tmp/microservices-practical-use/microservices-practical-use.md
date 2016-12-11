I've been getting very excited recently about Microservices.  It seems a natural progression, and culmination, of multiple architectural ideals.  For me, Microservices are more a state of mind at the moment rather than a practical implementation.  I would suspect over the coming years frameworks will be created or adapted to suit the Microservice architecture.

Until that happens, I though it would be fun to spend some time creating my own - thus giving me the opportunity to explore some of the benefits and, probably more importantly, the pitfalls.

This will be the first of a series of posts as I start to explore the the architecture.

### What Are Microservices?
Ha.  I'm not even going to attempt to answer that one.  There are greater minds than mine attempting to define the architecture.  If new to the term, I'd suggest having a search round the internet, but the best resource (in my opinion) is the book Building Microservices by Sam Newman:


![Image](/media/blog/microservices-practical-use/BuildingMicroservices.jpg)

This book will form the majority of the basis for my work.
### 
### Rules of the game
Before I start, it's worth outlining some of my planned rules.

While a environment based on Microservice architecture can (and probably should) be made up from various technologies, I will be sticking to Microsoft development tools (C#, MVC, WebAPI, etc).  This is for practicality purposes.  As a one-man team, it seems frivolous to mix and match technologies just for the sake of it.  That being said, should I find a use case that needs a different tool-set, then the architecture I plan to use will allow for it.

In his book, Sam talked about setting the rules of how the various Microservices should interact and behave in an enterprise environment.  For the purpose of my work, there are a few things that I want to define as "rules" before I start to build any services.

* Rule 1 - All services shall log to a centralised store
* Rule 2 - All services shall submit operational metrics to a centralised store
* Rule 3 - All services should be maintainable independently
* Rule 4 - All reusable code should be in centralised  libraries
* Rule 5 - All Microservices will be RESTful

### 
<h3>
</h3>### Centralised Logging
Any developer of any experience knows that log files are one of their most powerful tools.  No matter how much testing you perform, you will always get problem in the wild - and without log files you might as well be searching for the mythical needle.

Sams book suggests that a centralised log be used rather than having a trawl log files for each Microservice.  This is especially beneficial when a single "action" may call many Microservices - thus without some means of firstly logging to one place and secondly group them together, log tracking can be almost impossible.

Sam suggest logstash as a method of centralising those logs.  For my work, I will build a separate logging Microservice (with a DB store behind it).  I doubt my option would be robust enough to handle an enterprise solution - but could be a practical starting point.

To track an "action" across multiple Microservice, Sam suggests the use of a Correlation ID which is created at the start of the action, then passed through all Microservices until completion.  More on this once I work on the Logging Microservice.

I want to incorporate this into the Microservices from day 1, thus I'll build into a base WebAPI class (again more on this later in later posts).
### 
<h3>
</h3>### Centralised Metrics
Exactly the same principal as the logging, but possible less obvious - and easy to put to one side.

With even a reasonable complex estate, it will be easy for performance &amp; capacity management to be lost within the noise.  Thus if I provide metrics baked into the base WebAPI class, it should encourage me to take advantage of it.

Sam suggests a few options, but again I will create my own Microservice for now.
### 
<h3>
</h3>### Independence vs re-usable
Some of you will have noticed that Rule 3 &amp; 4 are somewhat at odds with each other.
On the one hand I want to benefit from code reuse (libraries) - yet on the other I want to avoid introducing a dependency that stops each service being worked on independently.
To underscore that last point; in a normal series of Visual Studio projects using a shared library, you will find that the library changes are included every-time that the dependent project is recompiled.  To allow Microservices to be managed independently, then this needs to be de-coupled.
Sam to the rescue again; he cites an example (todo - add reference if I can find it again) of a company using a git repository for centralised code - then forking that repository into the Microservice project - allowing the Microservice team to decide when to pull in the changes from the centralised repo.  This gives you the advantage of reusable code - but without the coupling.
One of the problems I foresee with this approach is managing the versions (centralised vs dependent projects).  How do I make sure that a dependent project is aware that a centralised library has been changed and should be evaluated?
This, and other questions, I hope to answer in my next post.### 
<h3>
</h3>### RESTful APIs
Nothing in Microservices suggest that you should use REST, RESTful or any other transport mechanism.  It is simply good practice to choose a small subset (1 or 2) and stick with them.  This means that there is a common method of communications between the Microservices - something that Sam advocates strongly.
It does however seem common that most people (from the examples I've found) are using REST or SOAP based webservices.  And as I intend to interact with some of these Microservice via JavaScript (AJAX) then it makes sense to go down the RESTful/ JSON payload option.### 
<h3>
</h3>### Next 
As alluded to above, my next article with be regarding the source management - how it is stored for reuse yet independence AND how I can manage those loosely-coupled inter-dependencies over a growing estate.
