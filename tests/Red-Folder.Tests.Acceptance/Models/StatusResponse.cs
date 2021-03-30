using System;

namespace Red_Folder.Tests.Acceptance.Models
{
    public class StatusResponse
    {
        public string Name { get; set; }
        public string InstanceId { get; set; }
        public string RuntimeStatus { get; set; }
        public CrawlRequest Input { get; set; }
        public string CustomStatus { get; set; }
        public CrawlResults Output { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}
