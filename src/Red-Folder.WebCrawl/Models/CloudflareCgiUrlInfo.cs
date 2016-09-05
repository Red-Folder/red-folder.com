using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    public class CloudflareCgiUrlInfo : BaseUrlInfo
    {
        public CloudflareCgiUrlInfo(string url)
        {
            _url = url;
        }
    }
}
