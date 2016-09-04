using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    public class UrlInfo
    {
        public string Url { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; } 
        public IList<string> Links { get; private set; }

        public UrlInfo(string url, HttpStatusCode httpStatusCode, IList<string> links)
        {
            Url = url;
            HttpStatusCode = httpStatusCode;
            Links = links;
        }

        public bool Valid
        {
            get
            {
                return HttpStatusCode == HttpStatusCode.OK;
            }
        }

        public bool HasLinks
        {
            get
            {
                return Links != null && Links.Count > 0;
            }
        }
    }
}
