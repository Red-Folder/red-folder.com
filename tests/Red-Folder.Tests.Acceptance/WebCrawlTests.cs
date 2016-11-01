using Red_Folder.Tests.Acceptance.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Red_Folder.Tests.Acceptance
{
    public class WebCrawlTests
    {
        [Fact]
        public void Check_For_Deadlinks()
        {
            var uat = new WebCrawlerBuilder("https://www.red-folder.com")
                            .Build();

            uat.Crawl();

            Assert.True(uat.Valid(), uat.ToString());
        } 
    }
}
