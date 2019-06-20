using Microsoft.AspNetCore.Mvc;
using CodeHollow.FeedReader;
using System.Threading.Tasks;

namespace RedFolder.Controllers.Web
{
    public class PodcastsController : Controller
    {
        public async  Task<ActionResult> Index()
        {
            var model = await FeedReader.ReadAsync("https://anchor.fm/s/b669760/podcast/rss");
            return View(model);
        }
    }
}