using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Red_Folder.Podcast;

namespace RedFolder.Podcast
{
    public class PodcastRespository : IPodcastRespository
    {
        private const string RSS_URL = "https://anchor.fm/s/b669760/podcast/rss";
        private readonly HttpClient _httpClient;
        private readonly IFeedReader _feedReader;

        private List<Models.Podcast> _cache;

        public PodcastRespository(IHttpClientFactory httpClientFactory, IFeedReader feedReader)
        {
            _httpClient = httpClientFactory.CreateClient("podcast");
            _feedReader = feedReader;
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
                await AddShowNotes(item);
            }

            return item;
        }

        public async Task<Models.Podcast> GetPodcast(int episodeNumber, bool includeShowNotes = false)
        {
            var feed = await GetOrLoadPodcasts();
            if (feed == null) return null;

            var item = feed.SingleOrDefault(x => x.EpisodeNumber == episodeNumber);

            if (includeShowNotes && item.ShowNotes == null)
            {
                await AddShowNotes(item);
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
                await AddShowNotes(item);
            }

            return item;
        }

        private async Task AddShowNotes(Models.Podcast podcast)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://content.red-folder.com/podcasts/{podcast.SafeUrl}.md");
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        // Fall back to the number notation i.e. 4.md
                        response = await _httpClient.GetAsync($"https://content.red-folder.com/podcasts/{podcast.EpisodeNumber}.md");

                        if (!response.IsSuccessStatusCode)
                        {
                            return;
                        }
                    }
                }

                var markdown = await response.Content.ReadAsStringAsync();

                HeyRed.MarkdownSharp.Markdown processor = new HeyRed.MarkdownSharp.Markdown();

                podcast.ShowNotes = processor.Transform(markdown);
            }
            catch
            {
            }
        }

        public async Task<Models.PodcastPromotion> PodcastPromotion()
        {
            return new Models.PodcastPromotion
            {
                LatestPodcast = await LatestPodcast(includeShowNotes: false),
                Metrics = new Models.PodcastMetrics
                {
                    NumberOfEpisodes = await NumberOfEpisodes(),
                    TotalDuration = await TotalPodcastDuration()
                }
            };
        }

        private async Task<int> NumberOfEpisodes()
        {
            var feed = await GetOrLoadPodcasts();
            if (feed == null) return 0;

            return feed.Count(x => x.FullEpisode);
        }

        private async Task<TimeSpan> TotalPodcastDuration()
        {
            var feed = await GetOrLoadPodcasts();
            if (feed == null) return TimeSpan.MinValue;

            var totalSeconds = feed.Where(x => x.FullEpisode).Sum(x => x.Duration);
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
                // TODO - Throw the exception for now. Prior code just hid it
                //try
                //{
                    var feed =  await _feedReader.Read(RSS_URL);

                    if (feed?.Items != null && feed.Items.Count > 0)
                    {
                        _cache = feed.Items.Select(x => (Models.Podcast)x).ToList();
                    }
                //} catch (Exception)
                //{

                //}
            }

            return _cache;
        }
    }
}
