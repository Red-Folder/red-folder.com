using Microsoft.AspNetCore.Mvc;
using RedFolder.ViewModels;
using System.Collections.Generic;

namespace RedFolder.Controllers.Web
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            var services = new List<Service>
            {
                new Service
                {
                    Title = "Leadership",
                    Description = new List<string>
                    {
                        "15+ years management experience",
                        "Team Lead to Head of IT",
                        "Scrum Master Certified",
                        "Agile/ Scrum coaching/ evangelism",
                        "Specialist in improving legacy teams"
                    }
                },
                new Service
                {
                    Title = "Architecture",
                    Description = new List<string>
                    {
                        "Architecting Microsoft Azure Solutions Certified",
                        "Considerable Solution Architecture experience",
                        "Patterns: Microservices, Web Application, Distributed",
                        "Technical mentoring and leadership",
                        "Reverse engineering legacy systems"
                    }
                },
                new Service
                {
                    Title = "Development",
                    Description = new List<string>
                    {
                        "20+ years development experience",
                        "Microsoft App Builder/ Web Applications Certified",
                        "ASP.Net, MVC, C#, .Net Core. WebAPI",
                        "React, Redux, Javascript, HTML, CSS",
                        "Specialist in handling legacy systems"
                    }
                },
                new Service
                {
                    Title = "DevOps",
                    Description = new List<string>
                    {
                        "Managed various Microsoft, Linux & Unix environments",
                        "Cloud Management – Microsoft Azure & Amazon AWS",
                        "CI/ CD tool - Team City, Jenkins, VSTS, Octopus",
                        "Database tools - SQL Server, MySQL, No SQL",
                        "Filling the gaps between technical teams"
                    }
                },
            };
            return View(services);
        }
    }
}