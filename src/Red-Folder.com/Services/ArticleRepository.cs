using RedFolder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public class ArticleRepository: IArticleRepository
    {
        public IEnumerable<BlogArticle> GetROIArticles()
        {
            return new List<BlogArticle>
                {
                    new BlogArticle(
                            "Business meet Software Development, Software Development meet Business",
                            "/images/roi/BusinessMeetDevelopment.png",
                            new Uri("https://www.linkedin.com/pulse/business-meet-software-development-mark-taylor"),
                            new Paragraphs
                            {
                                "An introduction to the series.  I cover why I've written it along with the intended audience."
                            }
                        ),
                    new BlogArticle(
                            "Why is software development so complex?",
                            "/images/roi/WhySoftwareDevelopmentIsComplex.png",
                            new Uri("https://www.linkedin.com/pulse/why-software-development-so-complex-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I take a look at what software development is and why it is so complex."
                            }
                        ),
                    new BlogArticle(
                            "What makes a great developer",
                            "/images/roi/WhatMakesAGoodDeveloper.png",
                            new Uri("https://www.linkedin.com/pulse/what-makes-great-developer-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I take a look at what makes a great developer.  I look at thier skills and possibly one of the most important parts of the series - motivation."
                            }
                        ),
                    new BlogArticle(
                            "What is Agile Software Development - Part 1",
                            "/images/roi/WhatIsAgile.png",
                            new Uri("https://www.linkedin.com/pulse/what-agile-software-development-part-1-mark-taylor"),
                            new Paragraphs
                            {
                                "I'm a great believe in the Agile movement with Software Development.  In this, the first of two parts I start to look at what that is."
                            }
                        ),
                    new BlogArticle(
                            "What is Agile Software Development - Part 2",
                            "/images/roi/WhatIsAgile.png",
                            new Uri("https://www.linkedin.com/pulse/what-agile-software-development-part-2-mark-taylor"),
                            new Paragraphs
                            {
                                "In the second part of looking at Agile, I look behind some of the fundemental principals."
                            }
                        ),
                    new BlogArticle(
                            "What is Security?",
                            "/images/roi/WhatIsSecurity.png",
                            new Uri("https://www.linkedin.com/pulse/what-security-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I take a quick look at security.  Everyone thinks security is important, right?  But are we putting enough investment into it?"
                            }
                        ),
                    new BlogArticle(
                            "The Zeigarnik Effect",
                            "/images/roi/TheZeigarnikEffect.png",
                            new Uri("https://www.linkedin.com/pulse/zeigarnik-effect-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I take a look at the Zeigarnik effect.  A psychological effect that can be used to better help understand Development Team motivation (and thus productivity)"
                            }
                        ),
                    new BlogArticle(
                            "Focus and Productivity",
                            "/images/roi/TheZeigarnikEffect.png",
                            new Uri("https://www.linkedin.com/pulse/focus-productivity-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I look at developer productivty.  I look at focus and flow - introducing some useful advice."
                            }
                        ),
                    new BlogArticle(
                            "Developer Anarchy",
                            "/images/roi/DeveloperAnarchy.png",
                            new Uri("https://www.linkedin.com/pulse/developer-anarchy-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I take a look at the Developer Anarchy movement.  I look at how it works and what could be learnt from it."
                            }
                        ),
                    new BlogArticle(
                            "ROI of failure",
                            "/images/roi/Failure.png",
                            new Uri("https://www.linkedin.com/pulse/roi-failure-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I look at the Return On Investment of failure.  We are taught from a young age to avoid failure.  Failrue is bad.  Or is it?  Is it not rather a useful tool in improvement?"
                            }
                        ),
                    new BlogArticle(
                            "ROI of the Hero Developer",
                            "/images/roi/HeroDeveloper.png",
                            new Uri("https://www.linkedin.com/pulse/roi-hero-developer-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I describe the Hero Developer, why they may seem like a hero, and why they maybe the villian of your ROI."
                            }
                        ),
                    new BlogArticle(
                            "ROI of building the thing right",
                            "/images/roi/BuildingTheThingRight.png",
                            new Uri("https://www.linkedin.com/pulse/roi-building-thing-right-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I turn the convertional wisdom of 'build the right thing' on its head.  I look at why 'build the thing right' maybe better for your ROI."
                            }
                        ),
                    new BlogArticle(
                            "The ROI of learning",
                            "/images/roi/Learning.png",
                            new Uri("https://www.linkedin.com/pulse/roi-learning-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I look at learning and why it is so important to ensure that your Development Team focus on it.  Learning is one of the greatest method to get the best Return on you Investment."
                            }
                        ),
                    new BlogArticle(
                            "When are you going to admit you are a technology company?",
                            "/images/roi/TechnologyCompany.png",
                            new Uri("https://www.linkedin.com/pulse/when-you-going-admit-technology-company-mark-taylor"),
                            new Paragraphs
                            {
                                "Are you a technology company?  No?  Are you sure?  In this article I take a look at why most business now should be considering themselves technology companies - and why that matters."
                            }
                        ),
                    new BlogArticle(
                            "Software development maturity model",
                            "/images/roi/MaturityModel.png",
                            new Uri("https://www.linkedin.com/pulse/software-development-maturity-model-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I take a look at maturity models and how they apply to software development.  How mature are you?"
                            }
                        ),
                    new BlogArticle(
                            "ROI of testing",
                            "/images/roi/Testing.png",
                            new Uri("https://www.linkedin.com/pulse/roi-testing-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I look at testing.  We all know that testing is important, right?  But do we really understand the investment needed for true quality?  Do we understand the ROI benefit that true quality can bring?"
                            }
                        ),
                    new BlogArticle(
                            "IT Market = Economic Shock",
                            "/images/roi/MarketShock.png",
                            new Uri("https://www.linkedin.com/pulse/market-economic-shock-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I look at how the IT jobs market can be a considerable economic shock to your business.  I also look at common reasons why this occurs.  Are you truly aware of the market value of your Software Development team?"
                            }
                        ),
                    new BlogArticle(
                            "ROI of Microsoft Certifications",
                            "/images/roi/Certifications.png",
                            new Uri("https://www.linkedin.com/pulse/roi-microsoft-certifications-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I look at Microsoft Certifications and how they have an effect on your ROI."
                            }
                        ),
                    new BlogArticle(
                            "ROI of Agile implementation",
                            "/images/roi/WhatIsAgile.png",
                            new Uri("https://www.linkedin.com/pulse/roi-agile-implementation-mark-taylor"),
                            new Paragraphs
                            {
                                "In this article I take a look at Agile implementation.  This is the start of taking the Agile principals discussed previously and bridging into techniques, processes and advice on how to deliver on those ideals."
                            }
                        )
            };    
        }

        public IEnumerable<BlogArticle> GetAspNetCoreArticles()
        {
            return new List<BlogArticle>
                {
                    new BlogArticle(
                            "My first Asp.Net Core app",
                            new Uri("/blog/converting-to-aspnet-core-part-1"),
                            new Paragraphs
                            {
                                "In the first of my articles, I build by first website using the RC1 version of Asp.Net Core."
                            }
                        ),
                    new BlogArticle(
                            "Converting my exisiting MVC 5 website",
                            new Uri("/blog/converting-to-aspnet-core-part-2"),
                            new Paragraphs
                            {
                                "In this article, I convert my existing MVC 5 website to run under Asp.Net Core RC1."
                            }
                        ),
                    new BlogArticle(
                            "Publishing to Azure",
                            new Uri("/blog/converting-to-aspnet-core-part-3"),
                            new Paragraphs
                            {
                                "In this article, I publish the website to Azure - completely replacing my previous MVC 5 version."
                            }
                        ),
                    new BlogArticle(
                            "A deeper dive into the Gulp Pipeline",
                            new Uri("/blog/converting-to-aspnet-core-part-4"),
                            new Paragraphs
                            {
                                "In this article, I take a deeper dive into the Gulp pipeline and how I can use it within my development process."
                            }
                        ),
                    new BlogArticle(
                            "Adding WebApi",
                            new Uri("/blog/converting-to-aspnet-core-part-5"),
                            new Paragraphs
                            {
                                "In this article, I add a simple WebApi (and a small amount of Angular)."
                            }
                        ),
                    new BlogArticle(
                            "Adding Entity Framework Core",
                            new Uri("/blog/converting-to-aspnet-core-part-6"),
                            new Paragraphs
                            {
                                "In this article, I add a simple database behind the WebApi using Entity Framework Core."
                            }
                        ),
                    new BlogArticle(
                            "RC1 -> RTM",
                            new Uri("/blog/converting-to-aspnet-core-part-7"),
                            new Paragraphs
                            {
                                "In the seventh, and last, of my articles of converting to Asp.Net Core, I’ll look at upgrading from RC1 to RTM."
                            }
                        ),
                };
        }
    }
}
