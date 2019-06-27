using Microsoft.AspNetCore.Mvc;
using CodeHollow.FeedReader;
using System.Threading.Tasks;
using System.Linq;
using RedFolder.Utils;
using System.Net.Http;
using RedFolder.com.ViewModels;
using CodeHollow.FeedReader.Feeds;

namespace RedFolder.Controllers.Web
{
    public class PodcastsController : Controller
    {
        private static HttpClient _httpClient = new HttpClient();

        public async Task<ActionResult> Index()
        {
            var model = await FeedReader.ReadAsync("https://anchor.fm/s/b669760/podcast/rss");
            return View(model);
        }

        public async Task<ActionResult> Podcast(string id = "Episode-0--Why-Im-doing-this-podcast")
        {
            var feed = await FeedReader.ReadAsync("https://anchor.fm/s/b669760/podcast/rss");

            var item = feed?.Items?.Single(x => SafeUrl.MakeSafe(x.Title) == id);
            if (item == null)
            {
                return new RedirectResult("/errors/status/404");
            }

            var model = new Podcast
            {
                Item = item,
                ShowNotes = await GetShowNotes(SafeUrl.MakeSafe(item.Title))
            };

            return View(model);
        }

        private async Task<string> GetShowNotes(string key)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://content.red-folder.com/podcasts/{key}.md");

                if (!response.IsSuccessStatusCode) return "";

                var markdown = await response.Content.ReadAsStringAsync();

                HeyRed.MarkdownSharp.Markdown processor = new HeyRed.MarkdownSharp.Markdown();

                return processor.Transform(markdown);
            }
            catch
            {
                return "";
            }
        }
    }
}