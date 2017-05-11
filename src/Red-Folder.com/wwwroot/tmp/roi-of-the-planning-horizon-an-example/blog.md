## Summary
Following on from last week’s article on [Planning Horizon](/blog/roi-of-the-planning-horizon), I wanted to provide an example.

So in this article, I follow a system across its lifetime and discusses both the drivers for change and the affect it has on the work.

## Meal Costing system
One of the first systems I ever produced was a Meal Costing system for an Inflight Catering company back in 1994/ 1995.

The system was built to allow the company to price up a variety of menus – which then provided quotes to their customers.  A menu would consist of a selection of ingredients – be they a pack of biscuits or the amount of potato required for the shepherd’s pie.

If writing this system today, I’d probably be able to provide most of the functionality with an Excel spreadsheet.

At the time though, Windows and Microsoft Excel were not mainstream.  I don’t think I had my first works PC until the end of the 90s.

At that point I was writing code for character based green screens:
![Terminal](/media/blog/roi-of-the-planning-horizon-an-example/Terminal.jpg)

I built the system using [Informix 4GL](https://en.wikipedia.org/wiki/IBM_Informix-4GL).  A reasonably well used language – but far from one of the major ones.

Initial effort to develop the system was probably around 8 weeks with various visits to customer site to tweak.  Largely speak though, by early 95 the software was in use and providing value.

So did the system stop there?  Was that all the investment that went in to it?

Of course not.

There was additional functionality required by the client over the remainder of 1995 – the odd day here … the odd week there.  Easily another 4 weeks of work.  Plus of course the costs of licences, support for the underlying hardware & software, electricity, etc.

## Time for the Crystal Ball
What follows from this point is speculation over the life of the system.  I left the software house that produced the software at the end of 1995 – so unfortunately, I don’t know the truth.  But I feel the below gives an idea of what that history could have looked like.

Ultimately the requirements for the system will be as true today as it was then.

## 1996 – The joy of Fax
Remember fax machines?  If not, lucky you.

But let’s assume that our client wants to be able to automatically fax their meal quotes to their customers at a push of button.

If you think that the meal costs may vary monthly or even weekly due to fluctuations in ingredient costs, it would be a fairly time consuming task for someone to print and then fax to every customer.  This automation would save them a fair amount of manual effort.

Let’s assume that it takes 2 weeks to implement.

## 1997 - Rise of Windows
The initial take up of Windows was fairly slow in most organisations – but once it took hold, everyone had their own personal computer.

And while you could still run “green screen” character based programs, users where expecting to have the productivity that came from Windows – and most importantly the mouse.

It would have been a natural evolution for the Meal Costing software to be re-written entirely to take advantage of the Windows innovation.

Spoiler alert – this is not likely to be the last re-write we see of the software during its lifetime.

While tools existed to “Window-fy” Informix 4gl – they were largely unsuccessful and fairly short lived.  So it would probably have been re-written in Visual Basic 6 from Microsoft.

Let’s assume this would have been another 4 weeks development.

## 1999 - Year 200 Bug
Anyone remember the panic over Y2K?

All computers were to fail, planes were to fall out of the sky and civilisation as we knew it would end.

While some of the claims were over-blown to say the least, the huge amount of work put in by IT professionals to validate and patch systems also helped.

I would have expected our client to want verification that the system would cope with Y2K – so let’s add another week for checking and testing the system.

## 2002 – The joy of Email
Fax is dead … long live email.

Let’s stop faxing and using email … it’s more convenient AND cheaper.

So another week of work to switch those Meal Costing quotes to email.

## 2005 – Moving to the Internet
The internet is gaining ground.  Customers are expecting to be able to just log on to a website to see their menus.  Possibly even have a greater variety available to them.

This really requires us to re-write the software again.  Visual Basic 6, while great on the Windows Desktop, really didn’t lend itself to websites … plus at this point it is really showing its age.

So I replace the entire system using VB.Net (the successor to Visual Basic 6) – a natural choice.

This is a major re-write and providing functionality not just for our client, but also their customers.

Let’s assume this take 8 weeks.

## 2006 – Electronic Data Interchange
Suppliers and customers want to deal electronically with our client and automate various supply chain tasks through [EDI](https://en.wikipedia.org/wiki/Electronic_data_interchange).

Let’s assume this is another 4 weeks.

## 2008 – Mobile Internet
Smartphones are on the rise.  From the humble beginnings of the XDA:
![XDA](/media/blog/roi-of-the-planning-horizon-an-example/xda.jpg)

An explosion of computers in our pockets.

Our client’s sales reps want to be able to capture customer data on their phone – rather than having to take manual notes, only to have to type them up later.

The website now also needs to be updated for the mobile phone screen size.

The system needs another re-write (or at least a good face lift) to do this.

Let’s assume this is another 4 weeks.

## 2010 –Social Media explosion
Facebook, Twitter, and a multitude of other Social Media tools … they are either here or coming.

Now this is a great time for our client to provide innovation to their customers.  Utilising Social Media they start to illicit feedback from their customer’s passengers.

Our client then uses that feedback to provide analysis to their customers on what is working well.  This is the start of providing intelligence to their customer – making them a valuable partner rather than just a supplier.

Let’s assume this is another 8 weeks.

## 2011 – Time for the iPad
And iPads make that job for our client’s sales reps so much easier.  And they introduce another screen size for the website.

Let’s assume this is another 4 weeks.

## 2012 – The joy of APIs
Our client’s customers and suppliers have enjoyed the benefits of the Electronic Data Interchange – but time marches on and [APIs](https://en.wikipedia.org/wiki/Application_programming_interface) are taking over from EDI.

Let’s assume another 4 weeks.

## 2014 – Device Tags
Adding [NFC](https://en.wikipedia.org/wiki/Near_field_communication) tags to meal containers allows our client to gather inventories faster – both on and off the plane.

It also allows collection of data of what meals appealed to what flights.

Again, this adds more detail to the analytic data our client can utilise to provide intelligence to their customers.

Let’s assume this is another 8 weeks.

## 2015 – JavaScript framework
JavaScript frameworks are making websites much more feature rich.  They have gained their reputation within high traffic systems like Facebook.

Using these enable a much high level of functionality for our client’s customer.  This opens the way for allowing a customer to generate their own menu through dragging and dropping various component parts.

This is yet another re-write.

Let’s assume this is another 4 weeks.

## 2016 – Internet Of Things
With IOT, everything can be intelligent – allowing even greater inventory and supply chain efficiencies.

Let’s assume this is another 4 weeks.

## 2017 – Artificial Intelligence
Our client has been collecting data for years.  They have been ahead of the market in providing their customers analytical data.

They see the advent of artificial intelligence and machine learning as the next step in providing even deeper insight to their customer.

Let’s assume this is another 4 weeks.

## Summary
Ok, I admit I’ve taken artistic licence with some of the dates and timescales – but hopefully the message is clear – the world changes and our software needs to change with it.

While our initial investment in the software was fairly minimal, the investment during its full lifetime dwarfs it almost into insignificance.  If you see the below, the original build accounts for just over 10% of the effort – and this really doesn’t take into account day-to-day maintenance (assume 10-25% on top of total development effort), licences, hardware, training, etc.

<table>
	<tr><th>Task</th><th style="text-align:right;">Weeks</th></tr>
	<tr><td>Original Build</td><td style="text-align:right;">8</td></tr>
	<tr><td>Snagging</td><td style="text-align:right;">4</td></tr>
	<tr><td>Fax</td><td style="text-align:right;">2</td></tr>
	<tr><td>Windows</td><td style="text-align:right;">4</td></tr>
	<tr><td>Y2K</td><td style="text-align:right;">1</td></tr>
	<tr><td>Email</td><td style="text-align:right;">1</td></tr>
	<tr><td>Website</td><td style="text-align:right;">8</td></tr>
	<tr><td>EDI</td><td style="text-align:right;">4</td></tr>
	<tr><td>Social Media</td><td style="text-align:right;">8</td></tr>
	<tr><td>Smartphone</td><td style="text-align:right;">4</td></tr>
	<tr><td>iPad</td><td style="text-align:right;">4</td></tr>
	<tr><td>API</td><td style="text-align:right;">4</td></tr>
	<tr><td>NFC</td><td style="text-align:right;">8</td></tr>
	<tr><td>JavaScript</td><td style="text-align:right;">4</td></tr>
	<tr><td>IOT</td><td style="text-align:right;">4</td></tr>
	<tr><td>AI</td><td style="text-align:right;">4</td></tr>
	<tr><th>Total</th><th style="text-align:right;">72</th></tr>
</table>


Thus, as I asked in my last article, why are we only looking at the immediate future when considering our planning (and thus ROI investment).

While I’d never expect anyone to plan over 20 years into the future – then certainly the next 3-5 years should be in mind when making todays decisions.

How will this decision affect our productivity and effectiveness over the coming week, months AND years.

Without thinking in this way, it is too easy to focus on the now and not correctly apply those best practices and principals that will produce the best ROI over the lifetime of the software.

## Applying that thinking
In the next article, I will give a summary of some of the practices and principals (most of which I've covered previously) and how this longer term mindset affects our planning.
