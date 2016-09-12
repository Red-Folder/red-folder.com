using Microsoft.Azure.WebJobs.Host;
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

        private TraceWriter _log;

        public ClientWrapper(TraceWriter log)
        {
            _log = log;
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

        public void Process(string url)
        {
            _log.Info(String.Format("Start of process of {0}", url));
            try
            {
                _log.Info("Creating HttpClientHandler");
                using (HttpClientHandler httpClientHanlder = new HttpClientHandler())
                {
                    httpClientHanlder.AllowAutoRedirect = false;

                    // https://social.msdn.microsoft.com/Forums/en-US/ca6372be-3169-4fb5-870f-bfbea605faf6/azure-webapp-webjob-exception-could-not-create-ssltls-secure-channel?forum=windowsazurewebsitespreview
                    _log.Info("Specifically setting Security Protocol to TLS 1.2");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    _log.Info("Creating HttpClient");
                    using (HttpClient client = new HttpClient(httpClientHanlder))
                    {
                        _log.Info("Creating HttpRequestMessage");
                        using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                        {
                            _log.Info("Sending request");
                            using (HttpResponseMessage response = client.SendAsync(request).Result)
                            {
                                _log.Info(String.Format("Prossing result - Http status {0}", response.StatusCode));
                                _lastHttpStatusCode = response.StatusCode;
                                _lastResponse = response.Content.ReadAsStringAsync().Result;
                                _lastHttpResponseHeaders = response.Headers;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Exception occured during Processing");
                _log.Error(String.Format("Excpetion message: {0}", ex.Message));
                _log.Error(String.Format("Stack trace: {0}", ex.StackTrace));
                throw ex;
            }
        }

    }
}
