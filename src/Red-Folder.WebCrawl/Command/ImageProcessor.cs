using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Red_Folder.WebCrawl.Helpers;
using Red_Folder.WebCrawl.Models;

namespace Red_Folder.WebCrawl.Command
{
    public class ImageProcessor : HttpClientBaseProcessor
    {
        public ImageProcessor(IClientWrapper httpClient) : base(httpClient)
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
            if (url.EndsWith(".png")) return true;
            if (url.EndsWith(".gif")) return true;
            if (url.EndsWith(".jpg")) return true;
            if (url.EndsWith(".gif")) return true;

            return false;
        }

        private IUrlInfo Handle(string url)
        {
            HttpGet(url);
            if (LastHttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ImageUrlInfo(url);
            }
            else
            {
                return new ImageUrlInfo(url, String.Format("Unexpected Status code: {0}", LastHttpStatusCode));
            }
        }
    }
}
