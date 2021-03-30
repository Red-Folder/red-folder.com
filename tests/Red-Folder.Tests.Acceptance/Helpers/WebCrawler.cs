using Newtonsoft.Json;
using Red_Folder.Tests.Acceptance.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace Red_Folder.Tests.Acceptance.Helpers
{
    public class WebCrawler
    {
        private const string ENDPOINT_START = "https://rfc-web-crawl.azurewebsites.net/api/Start";
        private const int SECONDS_TO_SLEEP_BETWEEN_RESULT_ATTEMPTS = 10 * 1000;     // 10 Seconds
        private const int RESULT_RETRIES = 100;

        private string _startKey = Environment.GetEnvironmentVariable("Red-Folder_Tests_Acceptance_CrawlWrapper_StartKey");

        private CrawlResults _results;

        public void Crawl(CrawlRequest crawlRequest)
        {
            _results = null;
            using (HttpClient client = new HttpClient())
            {
                var statusUri = Start(client, crawlRequest);

                _results = GetResultsWithRetry(client, statusUri, RESULT_RETRIES);
            }
        }

        public bool Valid()
        {
            return (ToString() == "Valid");
        }

        public override string ToString()
        {
            if (_results == null) return "Invalid - no results";
            if (_results.Urls == null) return "Invalid - no Urls";
            if (_results.Links == null) return "Invalid - no Links";

            var invalidUrls = _results.Urls.Where(x => !x.Valid).ToList();
            if (invalidUrls.Count > 0 )
            {
                StringBuilder response = new StringBuilder();
                response.AppendFormat("Invalid Urls found - {0}", invalidUrls.Count);
                response.AppendLine();

                foreach (var invalidUrl in invalidUrls)
                {
                    response.AppendFormat("{0} - {1}", invalidUrl.Path, invalidUrl.InvalidationMessage);

                    var referencedIn = _results.Links.Where(x => x.Target == invalidUrl.Path);
                    foreach (var reference in referencedIn)
                    {
                        response.AppendFormat("\tReferenced in: {0}", reference.Source);
                        response.AppendLine();
                    }
                }

                return response.ToString();
            }

            return "Valid";
        }

        private string Start(HttpClient client, CrawlRequest crawlRequest)
        {
            var statusUri = "";

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ENDPOINT_START))
            {
                request.Headers.Add("x-functions-key", _startKey);
                request.Content = new StringContent(JsonConvert.SerializeObject(crawlRequest), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    if (response.StatusCode == HttpStatusCode.Accepted)
                    {
                        var rawResults = response.Content.ReadAsStringAsync().Result;
                        var tmpObject = JsonConvert.DeserializeObject<StartResponse>(rawResults);
                        statusUri = tmpObject.StatusQueryGetUri;
                    }
                    else
                    {
                        throw new Exception($"Failed to start Crawl.  Received error code {response.StatusCode}");
                    }
                }
            }

            return statusUri;
        }

        private CrawlResults GetResultsWithRetry(HttpClient client, string statusUri, int retries)
        {
            CrawlResults results = null;

            int attempts = 0;
            while (results == null && attempts < retries)
            {
                // Sleep between each check
                Thread.Sleep(SECONDS_TO_SLEEP_BETWEEN_RESULT_ATTEMPTS);

                attempts++;

                results = GetResults(client, statusUri);
            }

            // If attempts have hit the retries and still no results, then throw an error
            if (results == null && attempts == retries)
            {
                throw new Exception(String.Format("Unable to received crawl results for {0} - have tried {1} times", statusUri, attempts));
            }

            return results;
        }

        private CrawlResults GetResults(HttpClient client, string statusUri)
        {
            CrawlResults results = null;

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, statusUri))
            {
                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Accepted:
                            break;
                        case HttpStatusCode.OK:
                            var rawResults = response.Content.ReadAsStringAsync().Result;
                            var statusResponse = JsonConvert.DeserializeObject<StatusResponse>(rawResults);

                            if (statusResponse.RuntimeStatus == "Completed")
                            {
                                results = statusResponse.Output;
                            }
                            else
                            {
                                throw new Exception($"Unexpected RuntimeStatus: {statusResponse.RuntimeStatus}");
                            }
                            break;
                        default:
                            throw new Exception($"Failed to get status for Crawl.  Received error code {response.StatusCode}");
                    }
                }
            }

            return results;
        }
    }
}
