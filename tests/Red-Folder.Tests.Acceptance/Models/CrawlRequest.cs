using System.Collections.Generic;

namespace Red_Folder.Tests.Acceptance.Models
{
    public class CrawlRequest
    {
        public string Host { get; set; }
        public List<string> HostSynonyms { get; set; }
    }
}
