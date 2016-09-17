using Newtonsoft.Json;
using Red_Folder.WebCrawl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Red_Folder.Tests.Acceptance.Helpers
{
    public class CrawlWrapper
    {
        private const string ENDPOINT_START = "https://rfc-webcrawl.azurewebsites.net/api/WebCrawlStart";
        private const string ENDPOINT_RESULTS = "https://rfc-webcrawl.azurewebsites.net/api/WebCrawlResults";
        private const int SECONDS_TO_SLEEP_BETWEEN_RESULT_ATTEMPTS = 10 * 1000;     // 10 Seconds
        private const int RESULT_RETRIES = 10;

        private string _startKey = Environment.GetEnvironmentVariable("Red-Folder.Tests.Acceptance.CrawlWrapper.StartKey");

        private CrawlResults _results;

        public CrawlWrapper()
        {

        }

        public void Crawl()
        {
            _results = null;
            using (HttpClient client = new HttpClient())
            {
                var crawlId = Start(client);

                _results = GetResultsWithRetry(client, crawlId, RESULT_RETRIES);
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

        private string Start(HttpClient client)
        {
            var crawlId = "";

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, ENDPOINT_START))
            {
                request.Headers.Add("x-functions-key", _startKey);

                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var rawResults = response.Content.ReadAsStringAsync().Result;
                        var tmpObject = JsonConvert.DeserializeObject<dynamic>(rawResults);
                        crawlId = tmpObject.id;
                    }
                    else
                    {
                        throw new Exception(String.Format("Failed to register for Crawl.  Received error code {0}", response.StatusCode));
                    }
                }
            }

            return crawlId;
        }

        private CrawlResults GetResultsWithRetry(HttpClient client, string crawlId, int retries)
        {
            CrawlResults results = null;

            int attempts = 0;
            while (results == null && attempts < retries)
            {
                // Sleep for 1 second each time
                Thread.Sleep(SECONDS_TO_SLEEP_BETWEEN_RESULT_ATTEMPTS);

                attempts++;

                results = GetResults(client, crawlId);
            }

            // If attempts have hit the retries and still no results, then throw an error
            if (results == null && attempts == retries)
            {
                throw new Exception(String.Format("Unable to received crawl results for {0} - have tried {1} times", crawlId, attempts));
            }

            return results;
        }

        private CrawlResults GetResults(HttpClient client, string crawlId)
        {
            CrawlResults results = null;

            string url = String.Format("{0}?id={1}", ENDPOINT_RESULTS, crawlId);

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var rawResults = response.Content.ReadAsStringAsync().Result;

                        results = JsonConvert.DeserializeObject<CrawlResults>(rawResults);
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            // No action.  Not found is a reasonable response if the processing hasn't completed yet
                        }
                        else
                        {
                            throw new Exception(String.Format("Failed to register for Crawl.  Received error code {0}", response.StatusCode));
                        }
                    }
                }
            }

            return results;
        }
    }
}
