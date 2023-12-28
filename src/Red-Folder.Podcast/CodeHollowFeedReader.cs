using CodeHollow.FeedReader;
using System.Threading.Tasks;

namespace Red_Folder.Podcast
{
    public class CodeHollowFeedReader : IFeedReader
    {
        public async Task<Feed> Read(string url)
        {
            return await FeedReader.ReadAsync(url);
        }
    }
}
