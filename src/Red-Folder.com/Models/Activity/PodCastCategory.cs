using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Red_Folder.com.Models.Activity
{
    public class PodCastCategory: IActivity
    {
        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("podCasts")]
        public List<PodCast> PodCasts { get; set; }

        [JsonIgnore]
        public long TotalDuration
        {
            get
            {
                if (PodCasts == null || PodCasts.Count() == 0) return 0;
                var total = PodCasts.Aggregate((long)0, (acc, x) => acc + x.EpisodeDuration);

                return total;
            }
        }
    }
}
