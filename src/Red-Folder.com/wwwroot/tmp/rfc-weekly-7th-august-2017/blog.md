Ok, so I’ve taken and passed the Microsoft 70-534 - Architecting Microsoft Azure Solutions exam.  It wasn’t easy – but I completed it with a score of 826 (700 being the pass mark).  It has been a challenging exam to pass, so I wanted to pass on some of my thoughts.

## NDA
Important to remember that as part of taking any Microsoft exam, you agree to an NDA to not divulge any of the questions.  So anything I talk about here will be either in the [exam overview](https://www.microsoft.com/en-gb/learning/exam-70-534.aspx) or covered in one of the training sources.

## Sources used
I’ve talked about most of these in the past 6 weeks.  I include a list here so that I can reference them through the rest of this document.

* [Pluralsight 70-534 Training Path](https://app.pluralsight.com/paths/certificate/azure-solutions-70-534)
* [Microsoft OpenEdx course](https://openedx.microsoft.com/courses/course-v1:Microsoft+DEV205Bx+2017_T2/about)
* [MeasureUp.com Practice test]( http://www.measureup.com/Architecting-Microsoft-Azure-Solutions-English-P5528.aspx)
* [Microsoft Virtual Academy: 70-534 – Certification Overview](https://mva.microsoft.com/en-US/training-courses/certification-exam-overview-70534-architecting-microsoft-azure-solutions-17406?l=XkymTWmjD_2306218965)
* [Microsoft Virtual Academy: 70-533 – Certification Overview](https://mva.microsoft.com/en-US/training-courses/certification-exam-overview-70533-implementing-microsoft-azure-infrastructure-solutions-17405?l=9TjC0QmjD_2606218965)
* [Microsoft Virtual Academy: 70-532 – Certification Overview](https://mva.microsoft.com/en-us/training-courses/certification-exam-overview-70532-developing-microsoft-azure-solutions-17404?l=iC64rDmjD_2606218965)

## DevOps
Ok, so something that became clear to me rather late in my study was how much this exam was DevOps focused.  You really do need to have some experience in both the development and the operations side of Azure – in addition to the high level architecture.

The MeasureUp.com practice test introduces you to this by having questions that require you to complete .Net code blocks or Powershell deployment scripts.

In hindsight, this does makes a lot of sense.  70-534 was meant to be the final of three exams to obtain the [MCSD: Azure Solution Architect](https://www.microsoft.com/en-gb/learning/mcsd-azure-architect-certification.aspx).  It was made up of three individual exams:

* [70-532: Developing Microsoft Azure Solutions](https://www.microsoft.com/en-gb/learning/exam-70-532.aspx)
* [70-533: Implementing Microsoft Azure Infrastructure Solutions](https://www.microsoft.com/en-gb/learning/exam-70-533.aspx)
* 70-534: Architecting Microsoft Azure Solutions

While in theory, you could have taken any of the exams in any order, the intention was clear in that you would work from 532 to 534 – or at least study all three in parallel.

It certainly felt like having studied 532 + 533 would have made 534 easier – but the same may well be true if you tried to start with one of the other exams.  Suffice to say that they are all heavily linked.

Note that MCSD: Azure Solutions Architect is no longer available – but the separate exams still stand.

So the main tip for me; go over the content for all three regardless of which exam you are taking.  Yes a lot more work, but it should make passing  significantly easier.

I also suspect that I would have a good start on 532 & 533 if I went straight into them – but I’ve other priorities that I want to focus on in the near term – so maybe something to come back to.

## Moving Feast
Azure is changing on a month by month basis.

The exam is not up to date with Azure.

And the training material is not up to date with the exam.

In my opinion, the MeasureUp.com practice test was the furthest out of date with the exam.  It covers a considerable amount of technology which is no longer in the exam.  It is however useful to learn so that you have a broad understanding of what may still be in legacy Azure accounts.

It may be worth reviewing the Microsoft Virtual Academy overviews and exam objectives before doing some of the other training – just so you can be aware when you are going into areas that have changed.

That is a lot more difficult than it sounds though until you have a reasonable understanding of what technologies are out there.

A bit of a chicken and egg scenario.

In the long run, I think you just have to accept you are likely to be learning about technologies that may have been retired (ACS for example) or greatly simplified.

## Exam is point in time
To push home the moving feast point, you have to take into account that some services will have changed since the exam was taken.

A couple of the training sources talked about Azure Storage Encryption and that it was only available on Azure Blog Storage – not File, Queue or Table.  Yet if you checked the [Azure Docs](https://docs.microsoft.com/en-us/azure/storage/storage-service-encryption), you would see that it was available on Blog & File.

For me, this sort of discrepancy is pretty much impossible to prepare for.

I would hope that something transient like that wouldn’t appear in the exam – but you simply don’t know.  Nor do I know how the exam would be marked for it.
My only advice is if you get a question like that, then just take a punt.

## Detail
Another oddity that came through on the MeasureUp.com practice tests where questions that would have required you having an encyclopaedic knowledge of the differences between the Azure SQL pricing tiers.

Trying to memorise a tier table is definitely not within my skillsets.

I get the impression that this maybe a hangover from a pervious version of the exam.  Although that being said, the Pluralsight courses does prompt you to try and memorise some differences between tiers – but just not to the same depth.

## Timings
If I’m honest, I did the exam quicker than I normally would.  I allowed 6 weeks from start to exam – normally I’d allow closer to double that.

Given the breadth of the exam this could have very easily been optimistic.

Roughly speaking, the time broke into:

* Weeks 1-2 – Pluralsight.com Learning Path
* Weeks 3-4 – OpenEdx course
* Week 5 – MeasureUp.com tests and a lot of reading
* Week 6 – MeasureUp.com tests and the MVA courses – and a lot of reading

The MeasureUp.com tests where great for giving me additional content to read.  In those last two weeks I read a lot of content (acknowledged that there was a good chance that a fair amount of it was out of date – but it still had value).

The MVA courses gave a nice refresher at the end to help with that final revision.

## If it has been different
So what would I have done if I had failed the exam?

I would have taken my earlier advice and studied the 532 & 533 exams.  I believe these would likely have filled any gaps I had in my knowledge – especially concerning actual code blocks.

## So what next
As I said above, I would expect to have a good start on taking the 532 & 533 exams.

So I may come back to them later in the year.

I see Azure being a very useful skill to have in the years to come.
