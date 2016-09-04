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
        private static string _url;

        private HttpStatusCode _lastHttpStatusCode = HttpStatusCode.OK;
        private string _lastResponse = null;
        private HttpResponseHeaders _lastHttpResponseHeaders;

        private string _relativePath;

        public ClientWrapper(string url)
        {
            _url = url;
        }

        public string GetLastUrl()
        {
            return _url;
        }

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

        public void Process()
        {
            using (HttpClientHandler httpClientHanlder = new HttpClientHandler())
            {
                httpClientHanlder.AllowAutoRedirect = false;

                using (HttpClient client = new HttpClient(httpClientHanlder))
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _url))
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
