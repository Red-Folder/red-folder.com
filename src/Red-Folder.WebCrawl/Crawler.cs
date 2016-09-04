using Red_Folder.WebCrawl.Helpers;
using Red_Folder.WebCrawl.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Red_Folder.WebCrawl
{
    public class Crawler
    {
        private IDictionary<string, UrlInfo> urls = new Dictionary<string, UrlInfo>();

        public Crawler()
        {

        }

        public void AddUrl(string url)
        {
            urls.Add(url, null);
        }

        public void Crawl()
        {
            while (urls.Where(x => x.Value == null).Count() > 0)
            {
                var urlsToAdd = new List<UrlInfo>();
                foreach (var key in urls.Where(x => x.Value == null).Select(x => x.Key))
                {
                    var client = new ClientWrapper(key);

                    client.Process();

                    var info = PopulateUrl(client);

                    urlsToAdd.Add(info);
                }

                // Update the Url items
                foreach (var urlInfo in urlsToAdd)
                {
                    urls[urlInfo.Url] = urlInfo;
                }

                // Populate with any new links
            }
        }

        public bool Valid()
        {
            return false;
        }

        public string Problems()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var url in urls.Keys)
            {
                var urlInfo = urls[url];

                if (urlInfo.HasLinks)
                {
                    foreach (var link in urlInfo.Links)
                    {
                        builder.AppendFormat("{0} links to {1}\n\r", url, link);
                    }
                }
            }

            return builder.ToString();
        }

        private UrlInfo PopulateUrl(ClientWrapper client)
        {
            var links = new LinksExtractor(client).Extract();

            return new UrlInfo(client.GetLastUrl(), client.GetLastHttpStatusCode(), links);
        }
    }
}
