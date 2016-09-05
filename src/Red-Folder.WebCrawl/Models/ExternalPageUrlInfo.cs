using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    public class ExternalPageUrlInfo : BaseUrlInfo
    {
        public ExternalPageUrlInfo(string url)
        {
            _url = url;
        }

        public ExternalPageUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
    }
}
