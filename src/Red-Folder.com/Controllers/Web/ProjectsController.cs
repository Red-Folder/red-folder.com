using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedFolder.ViewModels;
using RedFolder.Services;

namespace RedFolder.Controllers.Web
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            var recentProjects = new List<RecentProject>
            {
                new RecentProject
                {
                    Title = "Sunsetting",
                    Description = new List<string>
                    {
                        "Various complex legacy systems needed to functional while heavy investment into new platform.  Legacy platform consisting of a variety of systems"
                    },
                    Diciplines = new Diciplines
                    {
                        Leadership = 0,
                        Architecture = 20,
                        DevOps = 50,
                        Development = 30
                    },
                    Technologies = new List<string>
                    {
                        "C#",
                        "React",
                        "ASP.Net MVC/ WebApi",
                        "SQL Server",
                        "Javascript/ Typescript",
                        "AWS"
                    }
                },

                new RecentProject
                {
                    Title = "Enhanced security using Multi-Factor Authentication",
                    Description = new List<string>
                    {
                        "To enhance security for customers to implement Multi-Factor Authentication into legacy platform with minimal impact to customer journey or technical change.",
                        "Assessed various MFA solutions - including simple One-Time-Password systems like Google Authenticator through to complex feature right Authentication Platforms such as Twilio Authy, Auth0, and AWS Cognito.  Produced POC and implementation guidelines on how to integrate into legacy platform with minimal technical change"
                    },
                    Diciplines = new Diciplines
                    {
                        Leadership = 0,
                        Architecture = 80,
                        DevOps = 0,
                        Development = 20
                    },
                    Technologies = new List<string>
                    {
                        "C#"
                    }
                },

                new RecentProject
                {
                    Title = "Division for Sale",
                    Description = new List<string>
                    {
                        "Stalling project - struggling team",
                        "To sell off a division",
                        "Needed a complex customer facing application to make 'sellable'"
                    },
                    Diciplines = new Diciplines
                    {
                        Leadership = 50,
                        Architecture = 20,
                        DevOps = 20,
                        Development = 10
                    },
                    Technologies = new List<string>
                    {
                        "React",
                        "Redux",
                        "AWS"
                    }
                },

                new RecentProject
                {
                    Title = "Customer Verification Checks",
                    Description = new List<string>
                    {
                        "Regulatory changes required my client to implement customer identify & age checks at short notice.",
                        "This requirement the design and development of a platform that would utilise traditional credit check agencies and automated document verification partners."
                    },
                    Diciplines = new Diciplines
                    {
                        Leadership = 0,
                        Architecture = 40,
                        DevOps = 30,
                        Development = 30
                    },
                    Technologies = new List<string>
                    {
                        "C#",
                        "ASP.Net MVC/ WebApi",
                        "SQL Server",
                        "AWS"
                    }
                },

                new RecentProject
                {
                    Title = "Keeping the lights on during acquisition",
                    Description = new List<string>
                    {
                        "Managing of a legacy team and associated e-commerce systems during acquisition.",
                        "During the process, they had lost the previous management team and needs a stable pair of hands to keep the ship afloat - and implement a number of tactical technical changes."
                    },
                    Diciplines = new Diciplines
                    {
                        Leadership = 70,
                        Architecture = 20,
                        DevOps = 0,
                        Development = 10
                    },
                    Technologies = new List<string>
                    {
                        "C#",
                        "ASP.Net MVC/ WebApi",
                        "SQL Server",
                        "Javascript"
                    }
                },

                new RecentProject
                {
                    Title = "Feasibility study",
                    Description = new List<string>
                    {
                        "Client asked for a feasibility study to implement a change being requested by their partner.",
                        "Partner suggested the work was trivial",
                        "I assessed the impact on the client throughout their legacy platform - producing both a high level summary of the work a heatmap of impacted systems - highlighting the considerable effort and risk in the change."
                    },
                    Diciplines = new Diciplines
                    {
                        Leadership = 0,
                        Architecture = 100,
                        DevOps = 0,
                        Development = 0
                    },
                    Technologies = new List<string>()
                }
            };

            return View(recentProjects);
        }

        public ActionResult ROI([FromServices] IBlogRepository repository)
        {
            var blogs = repository.GetAll();
            var collection = new BlogCollection(blogs, 1, 0, "ROI", BlogCollection.OrderBy.PublishedAscending);
            return View(collection);
        }
    }
}