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
                        "Projects",
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
                        "Projects",
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
                    )
            };

            return View(tiles);
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