In this article, part of my series explaining better ROI from software development, I look at Security.

## Is Security important?

I remember a conversation with a CFO regarding security. The CFO was a bright, intelligent individual but held the belief that “security” was largely a made up problem to benefit the security industry – a fear to which to sell their wares.

To an extent I can understand his comments. Like any industry, the security industry, has marketed to an existing need. They may have overplayed that need in some cases, but that need does exist. That shouldn't be lost within marketing hyperbole.

Security is, and always has been, an important consideration for any organisation.

Basically; if you have something someone covets, then you need to consider if security is needed.

Imaging you have a pile of cash. Would you leave it out on a table in an empty room?

If that amount was worth pence, then maybe.

What if that amount was hundreds or thousands of pounds? Then you’d at least want a safe to put it in.

What if that amount was millions? Then you’d want a safe, armed guards, killer dogs and robot ninjas.

Your organisation will have various things that it should consider protecting. Be it corporate sensitive data, customer private information, your brand. Each organisation will be different.

Your organisation will likely have legal obligations. If you process credit card details for example, you will have legal liability in the processing and storage of any credit card details. The same with any individual’s personal information. Those obligations will come with penalties for infraction. Some will maybe relatively trivial – some may close your organisation. Those obligations will differ between territories – sometimes made even more complex if you and your customer are in different territories. I won’t dig into those in this article as it is outside of my expertise.

I’d rather focus on the moral obligations.

I believe a company shouldn't have to have a legal fine hanging over them to behave in a proper manner. A simple level of respect for your staff & customers will generally suffice in setting the correct tone.

Take a simple test – take any piece of information (data). How would you feel if it became public domain? If it was posted on Facebook for example?

If the minimum you would feel is embarrassment, then you need to protect it.

Some of you reading this maybe asking what this has to do with ROI. Fussy feelings don’t affect the bottom line.

Unfortunately, they do.

If your customers lose faith in your brand then that will be a major impact to your revenue. Not only will you likely have lost income, you will spend money & effort repairing/ reinforcing the brand, and possibly even compensating the customer – even if only in gestures of good will.

Even perception has a value when it comes to customer confidence in your brand.

## Being practical

When it comes to software security in your organisation, you need to assess what is important and how much security you can provide it.

When it comes to software security, there is a simple rule – you can slow, but you can’t stop.

There are some exceptionally clever people in the world of hacking – cleverer that you, your team and possible even the high priced security company that you deal with (watch Mr Robot on NetFlix).

If those individuals have enough desire and drive to get into your systems, then they will.

All the security you build in is an attempt to make it not worth their while.

This is true of any security.

We have locks on our doors and alarms on our homes to deter – not to stop. Largely we want to make it so they don’t bother and move on.

So again it comes down to what you have and how valuable it is. You then assess and decide the level of investment to put into that security (no robot ninjas for protecting the office coffee jar).

## How much is right?

This is pretty much an impossible one to answer.

I would however advise thinking about start your thinking based on maximum security, then reducing it down to appropriate. I’d favour this approach rather than start with none and justify adding it.

The former approach will at least ensure you think about and review the ROI.

The latter approach will invariable leave it light by avoiding the conversation.

## Accept that you know little

> “A little knowledge is a dangerous thing” Alexander Pope

I'm not a security expert.

You won’t be a security expert from reading this article – or probably hundreds of articles.

I questions sometimes if security experts are security experts.

As I said above, there are very very clever people out there. They are the experts and they are creating new ways to get at your valuables every day.

The point of this section isn't to scare you. It is rather to enforce that security is an ongoing, continual process.

I don’t want you to get a false sense of security by having spent a couple of days on it and then think you’re done.

You need to regularly review and assess your position.

## Make Security a first class concern

And this is probably the most important thing to take away from this article. Security should always be part of any software considerations.

You may choose to reduce the level of security on certain aspects – but this is inherently a safer place to be than to not have consider it at all.

You as the business should insist that security is considered.

You don’t have to personally be involved – either in the assessment or the level setting, but you should enable a culture where investment in security is encouraged – not be considered an indulgence.

I've talked in previous articles about Deming attributing 94% of performance to the system with only 6% to the individual. This is clearly a system thing.

The system should enforce that we take security seriously. Otherwise the assumption will always be that we can come back to security later.

## Software Development Process

Security is much more cost effective to add to a system as it is created. Adding it retrospectively will be considerably more expensive.

As such your software development process should have relevant checks in place to ensure that security has been considered AND that the right level has been adopted.

If you tie this in the Agile Software Development process I've discussed previously, the little and often delivery allows you to gauge (and adjust) that investment in security as you go.

At the very least you should have some check to ensure that any software that goes into production has been reviewed against your own security standard. Again, I would stress doing this as you go. Much better to assess as you go in a 12 month project than to add it to the end of the development.

## Training

So how do we get security into our software?

The title of this section somewhat gives it away – and it is simple enough, but we need to train our developers to think about security.

They need to be aware of, at the very least, fundamental security concerns.

Best back for your buck, train the development to think about it. This will normally involved external investment – someone ideally that comes in and works with the team on an ongoing basis to raise knowledge.

The development team should then be building security checkpoints into the development process. Key places would be in design, testing and sign-off.

If you operate a “show & tell” meeting to your software development, then security should be a specific section to which the development team feedback on what they have done and why.

You as a business manager need to enforce that this thinking is engrained and isn't bypassed.

## Isolation

Because of the sheer size of the security landscape; I'm only going to pick out a couple concepts. I may expand on others in future articles.

One of these is Isolation. In your software (or indeed your physical security) you always want to have multiple layers of security (like an onion skin). If someone penetrates the first layer of security, they should be presented with the next layer.

It is the same principal as a medieval castle; your first line of defence may be a moat. After a moat you have an outer wall. You may then have an inner wall. You may then have bailey, etc. Again, the more difficult that you make it, the more likely they will move on.

You can also think about bulkheads in submarines or ships. Should a submarine suffer damage and start taking on water – you close the bulkheads so that only the affected area is flooded – not the entire vessel. This way, yes you've suffered damage, but it isn't catastrophic.

In terms of your software; if your webserver is compromised, you don’t want the attacker to have immediate access to your database of customer credit card details. There should be further layers of security.

## Social hacking

> “Social hacking describes the act of attempting to manipulate outcomes of social behaviour through orchestrated actions. The general function of social hacking is to gain access to restricted information or to a physical space without proper permission.” [Wikipedia](https://en.wikipedia.org/wiki/Social_Hacking) 

Social hacking is possible one of the most important organisational considerations. We can developer software which can be defensible – but it is very difficult to train an entire organisation.

It is important to note that social hacking is a real thing – and very easy to accomplish – this isn't just the realm of TV & Film.

There are even tournaments being run in Social Hacking. This [article](http://www.social-engineer.org/how-tos/winning-sectf-def-con-22/) talks about one such tournament in which contestants receive points from retrieving information from an unsuspecting organisation. That information is graded in terms of “value” to a hacker – the more beneficial, the more points.

The organisation in question actually does this as part of an educational process – but it demonstrates how easy it is to get usable, valuable information through simply talking to someone over the phone.

> “Phishing is the attempt to acquire sensitive information such as usernames, passwords, and credit card details (and sometimes, indirectly, money), often for malicious reasons, by masquerading as a trustworthy entity in an electronic communication.” Wikipedia

Phishing is another method of gaining information or access to systems through social engineering. You may receive an email from your bank asking you click a link to change your password. In doing so you are redirected to a fake site which takes your bank credentials – effectively giving access to your bank account to someone else.

I've also heard about account payable departments receiving faked emails from an exec asking them to make an emergency off-off payment – and it being actioned.

Why does social hacking work?

We are inherently social people. We are brought up to be helpful and to do what we can. As long as what we are being asked sounds correct, then we are likely to do it.

Again this is a training thing. Both for your staff and for your customers.

## Monitoring and logging

Would you know if you’d been hacked?

Most organisations probably wouldn't.

If you take my medieval castle example above; if no-one is walking the walls, a single attacker can penetrate all your defences with a chisel, hammer and enough time.

You need to make sure that you have monitoring and logging in place for suspicious behaviour.

It is possible to purchase “Intrusion Prevention Systems” (IPS) which will monitor for that suspicious behaviour and possibly stop it. Anything like this should really be part of a wider security strategy – again something you really need external advice on.

> “Intrusion prevention systems (IPS), also known as intrusion detection and prevention systems (IDPS), are network security appliances that monitor network and/or system activities for malicious activity. The main functions of intrusion prevention systems are to identify malicious activity, log information about this activity, attempt to block/stop it, and report it” [Wikipedia](https://en.wikipedia.org/wiki/Intrusion_prevention_system)

If you know you are being hacked (or have been hacked) you can react to that fact – ideally to reduce an impact or liability.

## External audits

This is unfortunately where is can get quite expensive. You are paying for expertise.

Using third parties will always be expensive – but there is justifiable ROI in it.

You need to balance that expense against the risks of attack and customer confidence – think about it in the same way as insurance.

Having the external audits helps to make sure that you haven’t missed anything – plus they will generally provide you with a lot of security advice.

It can also open doors. A number of big organisations will want confidence that you have appropriate levels of security – especially if you are handling any form of their data. In those situations then it is basically a cost of doing business.

## ROI Summary

We've covered a lot a ground – yet really only scratched the surface on security. I wanted to end with a quick ROI summary:

* A successful attack can be so expensive that it is a extinction event (though fines, brand damage, financial liability)
* It is much more cost effective to build security into software as you go
* Lack of security will be a barrier to partnerships with large organisations