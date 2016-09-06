using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Red_Folder.WebCrawl.Models;

namespace Red_Folder.WebCrawl.Helpers
{
    public class RedirectLinksExtractor : ILinksExtractor
    {
        public IList<IUrlInfo> Extract(string url)
        {
            var links = new List<IUrlInfo>();
            links.Add(new AwaitingProcessingUrlInfo(url.ToLower().Trim()));
            return links;
        }
    }
}
