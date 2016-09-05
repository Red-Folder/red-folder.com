using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    public class PageUrlInfo: BaseUrlInfo
    {
        public PageUrlInfo(string url)
        {
            _url = url;
        }
        public PageUrlInfo(string url, IList<IUrlInfo> links)
        {
            _url = url;
            _links = links;
        }

        public PageUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
        public PageUrlInfo(string url, IList<IUrlInfo> links, string invalidationMessage)
        {
            _url = url;
            _links = links;
            _invalidationMessage = invalidationMessage;
        }
    }
}
