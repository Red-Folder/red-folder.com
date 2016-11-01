using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.Tests.Acceptance.Helpers
{
    public class WebClientBuilder
    {
        private string _host;
        private string _url;

        public WebClientBuilder(string host, string url)
        {
            _host = host;
            _url = url;
        }

        public WebClient Build()
        {
            return new WebClient(_host, _url);
        }
    }
}
