using Red_Folder.WebCrawl.Helpers;
using Red_Folder.WebCrawl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Command
{
    public class ContentProcessor : HttpClientBaseProcessor
    {
        public ContentProcessor(IClientWrapper httpClient) : base(httpClient)
        {
        }

        public override IUrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Handle(url);
            }
            else
            {
                return base.Process(url);
            }
        }

        private bool CanBeHandled(string url)
        {
            if (url.EndsWith(".css")) return false;
            if (url.EndsWith(".js")) return false;

            return false;
        }

        private IUrlInfo Handle(string url)
        {
            HttpGet(url);
            if (LastHttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ContentUrlInfo(url);
            }
            else
            {
                return new ContentUrlInfo(url, String.Format("Unexpected Status code: {0}", LastHttpStatusCode));
            }
        }
    }
}
