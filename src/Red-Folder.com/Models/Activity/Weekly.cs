using Newtonsoft.Json;
using System;

namespace Red_Folder.com.Models.Activity
{
    public class Weekly
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("weekNumber")]
        public int WeekNumber { get; set; }

        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }

        [JsonProperty("podCasts")]
        public PodCastActivity PodCasts { get; set; }
    }
}
