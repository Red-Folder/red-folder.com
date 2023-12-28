using CodeHollow.FeedReader;
using System.Threading.Tasks;

namespace Red_Folder.Podcast
{
    public interface IFeedReader
    {
        Task<Feed> Read(string url);
    }
}
