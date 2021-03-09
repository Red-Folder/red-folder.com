using RedFolder.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RedFolder.Services;
using System.Linq;
using System.Threading.Tasks;
using RedFolder.Podcast;

namespace RedFolder.Controllers.Web
{
    public class HomeController : Controller
    {
        private readonly IEmail _emailClient;
        private readonly IPodcastRespository _podcastRespository;
        private readonly ITokenVerification _tokenVerification;

        public HomeController(IEmail emailClient, 
                              IPodcastRespository podcastRespository,
                              ITokenVerification tokenVerification)
        {
            _emailClient = emailClient;
            _podcastRespository = podcastRespository;
            _tokenVerification = tokenVerification;
        }

        public async Task<ActionResult> Index()
        {
            var model = new HomePage();
            model.ContactForm = new ContactForm();
            model.LatestPodcast = await _podcastRespository.LatestPodcast();
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

        public ActionResult Repo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View(new ContactForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> Contact(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                if (await _tokenVerification.Valid(contactForm.RecaptureToken))
                {
                    if (await _emailClient.SendContactThankYou(contactForm))
                    {
                        return View("ThankYou", contactForm);
                    }
                }

                ViewBag.ErrorMessage = "You request has failed to send.  Please check your email address is correct.  Or contact me directly at mark@red-folder.com";
            }

            return View(contactForm);
        }
    }
}