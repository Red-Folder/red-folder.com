using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.Tests.Acceptance.Helpers
{
    public class WebCrawlerBuilder
    {
        private string _host;

        public WebCrawlerBuilder(string host)
        {
            _host = host;
        }

        public WebCrawler Build()
        {
            return new WebCrawler(_host);
        }
    }
}
