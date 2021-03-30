using Red_Folder.Tests.Acceptance.Helpers;
using Red_Folder.Tests.Acceptance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Red_Folder.Tests.Acceptance
{
    public class WebCrawlTests
    {
        [Fact]
        public void Check_For_Deadlinks()
        {
            var host = Environment.GetEnvironmentVariable("Red-Folder_Tests_Acceptance_Host");
            var hostSynonyms = Environment.GetEnvironmentVariable("Red-Folder_Tests_Acceptance_Host_Synonyms");

            var crawlRequest = new CrawlRequest
            {
                Host = string.IsNullOrWhiteSpace(host) ? "https://www.red-folder.com" : host,
                HostSynonyms = string.IsNullOrWhiteSpace(hostSynonyms) ? new List<string>() : hostSynonyms.Split(';').ToList()
            };

            var uat = new WebCrawler();

            uat.Crawl(crawlRequest);

            Assert.True(uat.Valid(), uat.ToString());
        } 
    }
}
