using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    public class ContentUrlInfo : BaseUrlInfo
    {
        public ContentUrlInfo(string url)
        {
            _url = url;
        }

        public ContentUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
    }
}
