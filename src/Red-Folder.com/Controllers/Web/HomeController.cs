using RedFolder.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RedFolder.Services;

namespace RedFolder.Controllers.Web
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomePage();
            model.Content = GetTiles();
            model.Contact = new ContactRequest();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Index(HomePage request)
        //{
        //    var model = new HomePage();
        //    model.Content = GetTiles();

        //    model.Contact = request.Contact;
        //    return View(model);
        //}

        private List<SummaryTile> GetTiles()
        {
            return new List<SummaryTile>()
            {
                new SummaryTile(
                        "Key Services",
                        new Paragraphs()
                        {
                            "Consultancy & Coaching on getting better ROI",
                            "Interim team leadership",
                            "Software Development"
                        },
                        "Services",
                        "Home",
                        "Find out more >>>",
                        "icon-services"
                    ),

                new SummaryTile(
                        "My Bio",
                        new Paragraphs()
                        {
                            "15+ years of senior IT management experience",
                            "20+ years of full software development life cycle experience",
                            "Microsoft Certified Professional: MCSD - Web Applications",
                            "Scrum Master Certified -Scrum.org PSM I",
                            "Specialist in improving legacy systems & teams"
                        },
                        "MyBio",
                        "Home",
                        "Find out more >>>",
                        "icon-bio"
                    ),

                new SummaryTile(
                        "Recent Projects",
                        new Paragraphs()
                        {
                            "Microservices",
                            "Cordova/ Phonegap"
                        },
                        "RecentProjects",
                        "Home",
                        "Find out more >>>",
                        "icon-projects"
                    )
            };

        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult MyBio()
        {
            var employment = new List<Employment>()
            {
                new Employment(
                        "Oct 2015", 
                        "Present",
                        "Lead Developer (Contract), Travelbag/ dnata Travel",
                        "Providing interim leadership to a cross-functional development team responsible for the Travelbag eCommerce websites.",
                        new List<string>()
                        {
                            "C#",
                            "ASP.Net MVC/ Web Forms/ WebAPI",
                            "SQL Server",
                            "HTML/ CSS/ JavaScript/ AJAX/ jQuery"
                        }
                    ),
                new Employment(
                        "Jul 2012",
                        "Aug 2015",
                        "Head of IT, Eazyfone Ltd/ Head of IT (Acting), Redeem Group",
                        "Responsible for the internal software products to operate the Redeem Group business of processing second hand mobile phone through testing, payment, warehousing and bulk sales.",
                        new List<string>()
                        {
                            "C#",
                            "ASP.Net MVC/ Web Forms/ WebAPI",
                            "SQL Server",
                            "HTML/ CSS/ JavaScript/ AJAX/ jQuery"
                        }
                    ),
                new Employment(
                        "Jun 2012",
                        "Jul 2012",
                        "Senior .Net Developer (Contract), Ipes",
                        "Responsible for maintenance and development of a bespoke fund management system.",
                        new List<string>() 
                        {
                            "C#",
                            "ASP.Net Web Forms",
                            "SQL Server",
                            "SharePoint"
                        }
                   ),
                new Employment(
                        "Sep 2000",
                        "Mar 2012",
                        "Head of IT, The Alternative Parcels Company Ltd",
                        "Responsible for the internal software products to operate the APC’s nationwide overnight courier network through its 120 partner depots.  Oversaw the activities of 13 staff and responsible for a £1m annual budget.",
                        new List<string>()
                        {
                            "C#",
                            "WCF",
                            "SQL Server",
                            "MySQL",
                            "PHP"
                        }
                    ),

                new Employment(
                        "Jun 2000",
                        "Aug 2008",
                        "IT Manager to Head of Development (Acting), Caudwell Group/ Pipex/ Tiscali",
                        "Responsible for various development teams & software products during series of acquisitions and mergers.",
                        new List<string>()
                        {
                            "C#",
                            "ASP.Net Web Forms",
                            "SQL Server",
                            "Oracle",
                            "MySQL",
                            "PHP",
                            "VB.Net",
                            "Informix 4gl"
                        }
                    ),
                new Employment(
                        "Jun 1999",
                        "Jun 2000",
                        "IT Infrastructure, George S. Hall Ltd."
                    ),
                new Employment(
                        "Nov 1995",
                        "Jun 1999",
                        "Analyst & Developer, Caudwell Group."
                    ),
                new Employment(
                        "Aug 1993",
                        "Nov 1995",
                        "Developer, PFS Computer Consultancy (Sussex) Ltd."
                    )
            };
            return View(employment);
        }

        public ActionResult RecentProjects()
        {
            var tiles = new List<SummaryTile>()
            {
                new SummaryTile(
                        "Return On Investment",
                        new Paragraphs()
                        {
                            "As a 20+ year veteran of software development I know the difference to the bottom line it can make.  I also know what a difference the right investment approach to Software Development can make.",
                            "Software Development is a complex and challenging process.  And it can seem like a frustrating activity when you are funding it – often seeming like a bottomless pit.",
                            "Traditional management approaches struggle with getting the best Return On Investment – if anything they are counterproductive.",
                            "In this series of articles I explain why and how to improve it.",
                        },
                        "ROI",
                        "Home",
                        "Find out more >>>"
                    ),
                new SummaryTile(
                        "Asp.Net Core",
                        new Paragraphs()
                        {
                            "The Microsoft .Net development environment has been with us for over 10 years.  During that time many innovations and changes have occurred.",
                            "Now is the time for the next evolution of that environment - Core.",
                            "Over the coming months and years, Microsoft will moving their focus away from the traditional .Net model to the.Net Core - which aims to be smaller, more composable and a better fit for modern software development.",
                            "In this series I start to look as Asp.Net Core, MVC 6 and Entity Framework Core by convering this website.I start off in RC1(Release Candidate 1) and conclude the article using RTM."
                        },
                        "AspNetCore",
                        "Home",
                        "Find out more >>>"
                    ),
                new SummaryTile(
                        "Microservice",
                        new Paragraphs() 
                        {
                            "I like the principals behind the MicroService architecture.  So I thought I'd experiment with it in my own code.  I've been working on a number of projects (which you can find in GitHub) which provide a series of example implementations."
                        },
                        "Index",
                        "Microservice",
                        "Find out more >>>"
                    ),
                new SummaryTile(
                        "Cordova/ Phonegap",
                        new Paragraphs() 
                        {
                            "I've written (or atleast started to write) a number Cordova plugins.",
                            "The most notable of which is the Background Service (BGS-Core), a plugin that allow Cordova applications to interact with Background Services on the Android platform.  I've personally used with clients to provide GPS tracking services.",
                            "While I'm not actively working on these plugins currently, there are still used by the community - and provide support when able."
                        },
                        "Index",
                        "Cordova",
                        "Find out more >>>"
                    ),
            };

            return View(tiles);
        }

        public ActionResult Repo()
        {
            return View();
        }

        public ActionResult ROI([FromServices]IArticleRepository repository)
        {
            return View(repository.GetROIArticles());
        }
        public ActionResult AspNetCore([FromServices]IArticleRepository repository)
        {
            return View(repository.GetAspNetCoreArticles());
        }
    }
}