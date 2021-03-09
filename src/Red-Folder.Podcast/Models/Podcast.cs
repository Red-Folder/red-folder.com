using CodeHollow.FeedReader;
using System;
using System.Linq;

namespace RedFolder.Podcast.Models
{
    public class Podcast
    {
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string PublishingDateString { get; set; }

        public string AudioUrl { get; set; }
        public string AnchorUrl { get; set; }
        public int Duration { get; set; }

        public string ShowNotes { get; set; }

        public string SafeUrl => Utils.SafeUrl.MakeSafe(Title);

        public static implicit operator Podcast(FeedItem item)
        {
            if (item == null || item.Title == null)
            {
                return null;
            }

            var rssItem = item.SpecificItem as CodeHollow.FeedReader.Feeds.Rss20FeedItem;

            var elements = item.SpecificItem.Element.Descendants().ToList();
            var episodeNumber = Int32.Parse(elements.FirstOrDefault(y => y.Name.LocalName == "episode")?.Value ?? "-1");
            var duration = Int32.Parse(elements.FirstOrDefault(y => y.Name.LocalName == "duration").Value);

            return new Podcast
            {
                EpisodeNumber = episodeNumber,
                Title = item.Title,
                Description = item.Description.Split(new string[] { "-----" }, StringSplitOptions.None).First(),
                PublishingDate = item.PublishingDate,
                PublishingDateString = item.PublishingDateString,
                AudioUrl = rssItem?.Enclosure?.Url,
                AnchorUrl = rssItem?.Link,
                Duration = duration
            };
        }
    }
}
