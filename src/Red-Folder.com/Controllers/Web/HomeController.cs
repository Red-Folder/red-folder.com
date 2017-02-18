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
            var redirect = repository.GetRedirects().Where(x => x.Value.Where(y => y.Url.ToLower() == url.ToLower() && y.RedirectByParameter).Count() > 0);
            if (redirect == null || redirect.Count() == 0)
            {
                return new RedirectResult("/errors/status/404");
            }
            else
            {
                var redirectUrl = "/blog/" + redirect.First().Key;
                var perm = redirect.First().Value.Where(y => y.Url.ToLower() == url.ToLower() && y.RedirectByParameter).First().RedirectType == System.Net.HttpStatusCode.MovedPermanently;
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
                            "15+ years senior IT management",
                            "20+ years software development life cycle",
                            "Microsoft Certified Professional",
                            "Scrum Master Certified",
                            "Specialist in legacy systems & teams"
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
                            "Return On Investment",
                            "Asp.Net Core Migration",
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