using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    [DataContract]
    public class CrawlResults
    {
        [DataMember(Name="id")]
        public string Id { get; private set; }
        [DataMember(Name = "timestamp")]
        public DateTime Timestamp { get; private set; }
        [DataMember(Name = "urls")]
        public IReadOnlyList<Url> Urls { get; private set; }
        [DataMember(Name = "links")]
        public IReadOnlyList<Link> Links { get; private set; }

        public CrawlResults(string id, List<Url> urls, List<Link> links)
        {
            Id = id;
            Timestamp = DateTime.UtcNow;
            Urls = urls.AsReadOnly();
            Links = links.AsReadOnly();
        }
    }
}
