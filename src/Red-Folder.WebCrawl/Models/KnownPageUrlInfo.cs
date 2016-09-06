using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    public class KnownPageUrlInfo : BaseUrlInfo
    {
        public KnownPageUrlInfo(string url)
        {
            _url = url;
        }

        public KnownPageUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
    }
}
