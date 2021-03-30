using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Red_Folder.Tests.Acceptance.Models
{
    public class CrawlResults
    {
        [JsonProperty("Host")]
        public string Host { get; private set; }
        
        [JsonProperty("Timestamp")]
        public DateTime Timestamp { get; private set; }

        [JsonProperty("Urls")]
        public List<Url> Urls { get; private set; }

        [JsonProperty("Links")]
        public List<Link> Links { get; private set; }
    }
}
