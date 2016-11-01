using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Red_Folder.Tests.Acceptance.Helpers
{
    public class WebClient
    {
        private static string _baseUrl;

        private HttpStatusCode _lastHttpStatusCode = HttpStatusCode.OK;
        private string _lastResponse = null;
        private HttpResponseHeaders _lastHttpResponseHeaders;

        private string _relativePath;

        public WebClient(string host, string relativePath)
        {
            _baseUrl = host;
            _relativePath = relativePath;
        }

        public HttpStatusCode LastHttpStatusCode
        {
            get
            {
                return _lastHttpStatusCode;
            }
        }

        public string LastResponse
        {
            get
            {
                return _lastResponse;
            }
        }

        public HttpResponseHeaders LastHttpResponseHeaders
        {
            get
            {
                return _lastHttpResponseHeaders;
            }
        }

        public void Get()
        {
            using (HttpClientHandler httpClientHanlder = new HttpClientHandler())
            {
                httpClientHanlder.AllowAutoRedirect = false;

                using (HttpClient client = new HttpClient(httpClientHanlder))
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _baseUrl + _relativePath))
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
