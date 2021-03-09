using Microsoft.AspNetCore.Mvc;
using RedFolder.Podcast;
using System.Threading.Tasks;

namespace RedFolder.Controllers.Web
{
    public class PodcastsController : Controller
    {
        private readonly IPodcastRespository _podcastRepository;

        public PodcastsController(IPodcastRespository podcastRespository)
        {
            _podcastRepository = podcastRespository;
        }

        public async Task<ActionResult> Index(string id = null, bool reload = false)
        {
            if (reload)
            {
                _podcastRepository.Clear();
            }

            if (string.IsNullOrEmpty(id))
            {
                var feed = await _podcastRepository.GetPodcasts();
                return View(feed);
            }

            return await Details(id);
        }

        private async Task<ActionResult> Details(string id)
        {
            var podcast = await _podcastRepository.GetPodcast(id, true);

            if (podcast == null)
            {
                return new RedirectResult("/errors/status/404");
            }

            return View("Details", podcast);
        }

        public ActionResult Roadmap()
        {
            return Redirect("https://podcast-roadmap.red-folder.com/");
        }
    }
}