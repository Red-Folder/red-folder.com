using RedFolder.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RedFolder.Services;
using System.Linq;

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

        public ActionResult Redirect([FromServices] IRedirectRepository repository, string url)
        {
            var redirect = repository.GetRedirects().Where(x => x.Value.Where(y => y.Url == url && y.RedirectByParameter).Count() > 0);
            if (redirect == null || redirect.Count() == 0)
            {
                return new RedirectResult("/errors/status/404");
            }
            else
            {
                var redirectUrl = "/blog/" + redirect.First().Key;
                var perm = redirect.First().Value.Where(y => y.Url == url && y.RedirectByParameter).First().RedirectType == System.Net.HttpStatusCode.MovedPermanently;
                return new RedirectResult(redirectUrl, perm);
            }
        }

        public ActionResult Throw()
        {
            throw new System.Exception("I'm not a real action");
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
                        "Index",
                        "Services",
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
                        "Index",
                        "MyBio",
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
                        "Index",
                        "Projects",
                        "Find out more >>>",
                        "icon-projects"
                    )
            };

        }

        public ActionResult Repo()
        {
            return View();
        }
    }
}