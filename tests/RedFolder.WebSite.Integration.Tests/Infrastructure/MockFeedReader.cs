using CodeHollow.FeedReader;
using Red_Folder.Podcast;
using System.Threading.Tasks;

namespace RedFolder.WebSite.Integration.Tests.Infrastructure
{
    public class MockFeedReader : IFeedReader
    {
        public async Task<Feed> Read(string url)
        {
            return await Task.FromResult(FeedReader.ReadFromFile("Data/better-roi-from-software-development.rss"));
        }
    }
}
