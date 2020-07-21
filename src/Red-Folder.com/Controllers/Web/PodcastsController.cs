using Microsoft.AspNetCore.Mvc;
using CodeHollow.FeedReader;
using System.Threading.Tasks;
using System.Linq;
using RedFolder.Utils;
using System.Net.Http;
using RedFolder.com.ViewModels;
using RedFolder.Services;

namespace RedFolder.Controllers.Web
{
    public class PodcastsController : Controller
    {
        private readonly IPodcastRespository _podcastRepository;

        public PodcastsController(IPodcastRespository podcastRespository)
        {
            _podcastRepository = podcastRespository;
        }

        public async Task<ActionResult> Index(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                var feed = await _podcastRepository.GetFeed();
                return View(feed);
            }

            return await Details(id);
        }

        private async Task<ActionResult> Details(string id)
        {
            var podcast = await _podcastRepository.GetPodcast(id);

            if (podcast == null)
            {
                return new RedirectResult("/errors/status/404");
            }

            return View("Details", podcast);
        }
    }
}