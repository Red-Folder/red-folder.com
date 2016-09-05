using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    public class UnknownUrlInfo : BaseUrlInfo
    {
        public UnknownUrlInfo(string url)
        {
            _url = url;
            _invalidationMessage = "Unknown url type";
        }
    }
}
