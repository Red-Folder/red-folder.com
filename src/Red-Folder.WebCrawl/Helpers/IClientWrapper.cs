using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Helpers
{
    public interface IClientWrapper
    {
        string GetLastUrl();
        HttpStatusCode GetLastHttpStatusCode();
        string GetLastResponse();
        HttpResponseHeaders GetLastHttpResponseHeaders();
        void Process();
    }
}
