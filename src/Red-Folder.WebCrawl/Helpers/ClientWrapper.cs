using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Helpers
{
    public class ClientWrapper: IClientWrapper
    {
        private HttpStatusCode _lastHttpStatusCode = HttpStatusCode.Unused;
        private string _lastResponse = null;
        private HttpResponseHeaders _lastHttpResponseHeaders;

        public HttpStatusCode GetLastHttpStatusCode()
        {
            return _lastHttpStatusCode;
        }

        public string GetLastResponse()
        {
            return _lastResponse;
        }

        public HttpResponseHeaders GetLastHttpResponseHeaders()
        {
            return _lastHttpResponseHeaders;
        }

        public void Process(string url)
        {
            using (HttpClientHandler httpClientHanlder = new HttpClientHandler())
            {
                httpClientHanlder.AllowAutoRedirect = false;

                using (HttpClient client = new HttpClient(httpClientHanlder))
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                    {
                        using (HttpResponseMessage response = client.SendAsync(request).Result)
                        {
                            _lastHttpStatusCode = response.StatusCode;
                            _lastResponse = response.Content.ReadAsStringAsync().Result;
                            _lastHttpResponseHeaders = response.Headers;
                        }
                    }
                }
            }
        }

    }
}
