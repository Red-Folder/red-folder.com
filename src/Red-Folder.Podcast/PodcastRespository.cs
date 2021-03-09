using System;
using CodeHollow.FeedReader;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RedFolder.Podcast
{
    public class PodcastRespository : IPodcastRespository
    {
        private const string RSS_URL = "https://anchor.fm/s/b669760/podcast/rss";
        private readonly HttpClient _httpClient;

        private List<Models.Podcast> _cache;

        public PodcastRespository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Models.Podcast>> GetPodcasts()
        {
            return await GetOrLoadPodcasts();
        }

        public async Task<Models.Podcast> GetPodcast(string id, bool includeShowNotes = false)
        {
            var feed = await GetOrLoadPodcasts();
            if (feed == null) return null;

            var item = feed.SingleOrDefault(x => x.SafeUrl == id);

            if (includeShowNotes && item.ShowNotes == null)
            {
                item.ShowNotes = await GetShowNotes(item.SafeUrl);
            }

            return item;
        }

        public async Task<Models.Podcast> LatestPodcast(bool includeShowNotes = false)
        {
            var feed = await GetOrLoadPodcasts();
            if (feed == null) return null;

            var item = feed.OrderByDescending(x => x.PublishingDate).FirstOrDefault();

            if (includeShowNotes && item.ShowNotes == null)
            {
                item.ShowNotes = await GetShowNotes(item.SafeUrl);
            }

            return item;
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
            var feed = await GetOrLoadPodcasts();
            if (feed == null) return 0;

            return feed.Count;
        }

        public async Task<System.TimeSpan> TotalPodcastLength()
        {
            var feed = await GetOrLoadPodcasts();
            if (feed == null) return TimeSpan.MinValue;

            var totalSeconds = feed.Sum(x => x.Duration);
            return TimeSpan.FromSeconds(totalSeconds);
        }

        public void Clear()
        {
            _cache = null;
        }

        private async Task<List<Models.Podcast>> GetOrLoadPodcasts()
        {
            if (_cache == null)
            {
                try
                {
                    var feed =  await FeedReader.ReadAsync(RSS_URL);

                    if (feed?.Items != null && feed.Items.Count > 0)
                    {
                        _cache = feed.Items.Select(x => (Models.Podcast)x).ToList();
                    }
                } catch (Exception)
                {

                }
            }

            return _cache;
        }
    }
}
