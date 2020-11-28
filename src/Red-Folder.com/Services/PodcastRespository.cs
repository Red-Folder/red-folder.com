using System;
using CodeHollow.FeedReader;
using RedFolder.com.ViewModels;
using RedFolder.Utils;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public class PodcastRespository : IPodcastRespository
    {
        private const string RSS_URL = "https://anchor.fm/s/b669760/podcast/rss";
        private readonly HttpClient _httpClient;
        
        public PodcastRespository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Feed> GetFeed()
        {
            return await FeedReader.ReadAsync(RSS_URL);
        }

        public async Task<Podcast> GetPodcast(string id)
        {
            var feed = await GetFeed();

            var item = feed?.Items?.Single(x => SafeUrl.MakeSafe(x.Title) == id);

            return await ToPodcast(item);
        }

        public async Task<Podcast> LatestPodcast()
        {
            var feed = await GetFeed();

            var latestItem = feed?.Items?.OrderByDescending(x => x.PublishingDate)?.FirstOrDefault();

            return await ToPodcast(latestItem);
        }

        private async Task<Podcast> ToPodcast(FeedItem item)
        {
            if (item == null || item.Title == null)
            {
                return null;
            }

            return new Podcast
            {
                Item = item,
                ShowNotes = await GetShowNotes(SafeUrl.MakeSafe(item.Title))
            };
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

        public async Task<int> NumberOfPodcasts()
        {
            var feed = await GetFeed();

            return feed.Items.Count;
        }

        public async Task<System.TimeSpan> TotalPodcastLength()
        {
            var feed = await GetFeed();

            var durationElements = feed.Items.Select(x => x.SpecificItem.Element.Descendants().FirstOrDefault(y => y.Name.LocalName == "duration"));
            var durations = durationElements.Where(x => x != null && x.Value != null).Select(x => Int32.Parse(x.Value));
            var totalSeconds = durations.Sum();

            return System.TimeSpan.FromSeconds(totalSeconds);
        }
    }
}
