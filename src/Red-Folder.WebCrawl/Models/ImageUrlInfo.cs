using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    public class ImageUrlInfo : BaseUrlInfo
    {
        public ImageUrlInfo(string url)
        {
            _url = url;
        }

        public ImageUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
    }
}
