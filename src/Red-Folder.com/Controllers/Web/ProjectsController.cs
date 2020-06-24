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
                    )
            };

            return View(tiles);
        }

        public ActionResult ROI([FromServices] IBlogRepository repository)
        {
            var blogs = repository.GetAll();
            var collection = new BlogCollection(blogs, 1, 0, "ROI", BlogCollection.OrderBy.PublishedAscending);
            return View(collection);
        }
    }
}