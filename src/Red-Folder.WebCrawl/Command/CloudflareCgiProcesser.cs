using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Red_Folder.WebCrawl.Models;

namespace Red_Folder.WebCrawl.Command
{
    public class CloudflareCgiProcesser : BaseProcessor
    {
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
            if (url.StartsWith(@"https://www.red-folder.com/cdn-cgi")) return true;

            return false;
        }

        private IUrlInfo Handle(string url)
        {
            return new CloudflareCgiUrlInfo(url);
        }
    }
}
