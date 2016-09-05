﻿using Red_Folder.WebCrawl.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Command
{
    public partial class HttpClientBaseProcessor : BaseProcessor
    {
        private IClientWrapper _httpClient;

        public HttpClientBaseProcessor(IClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        protected void HttpGet(string url)
        {
            _httpClient.Process(url);
        }

        protected HttpStatusCode LastHttpStatusCode
        {
            get
            {
                return _httpClient.GetLastHttpStatusCode();
            }
        }

        protected string LastHttpResponse
        {
            get
            {
                return _httpClient.GetLastResponse();
            }
        }

        protected HttpResponseHeaders LastHttpResponseHeaders
        {
            get
            {
                return _httpClient.GetLastHttpResponseHeaders();
            }
        }

    }
}
