using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedFolder.Podcast
{
    public interface IPodcastRespository
    {
        Task<List<Models.Podcast>> GetPodcasts();
        Task<Models.Podcast> GetPodcast(string id, bool includeShowNotes = false);
        Task<Models.Podcast> LatestPodcast(bool includeShowNotes = false);
        Task<int> NumberOfPodcasts();
        Task<TimeSpan> TotalPodcastLength();
        
        void Clear();
    }
}