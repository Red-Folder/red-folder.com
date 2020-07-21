using System.Threading.Tasks;
using CodeHollow.FeedReader;
using RedFolder.com.ViewModels;

namespace RedFolder.Services
{
    public interface IPodcastRespository
    {
        Task<Feed> GetFeed();
        Task<Podcast> GetPodcast(string id);
        Task<Podcast> LatestPodcast();
    }
}