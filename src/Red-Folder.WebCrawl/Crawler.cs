using Red_Folder.WebCrawl.Command;
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
        private IDictionary<string, IUrlInfo> urls = new Dictionary<string, IUrlInfo>();
        private int _maxDepth = 10;
        private string _websiteDomain = @"https://www.red-folder.com";
        private string _blogDomain = @"http://blog.red-folder.com";
        private string _oldBlogDomain = @"http://red-folder.blogspot.co.uk";
        private string _githubDomain = @"https://github.com/red-folder";


        private IProcessUrl _processor;

        private string _problems = "";

        public Crawler()
        {
            var internalDomains = new List<string>
            {
                _websiteDomain,
                _blogDomain,
                _oldBlogDomain,
                _githubDomain
            };

            _processor = new CloudflareCgiProcesser()
                            .Next(new ImageProcessor(new ClientWrapper())
                            .Next(new ContentProcessor(new ClientWrapper())
                            .Next(new KnownPageProcessor()
                            .Next(new ExternalPageProcessor(internalDomains)
                            .Next(new PageProcessor(_githubDomain, new ClientWrapper(), null)
                            .Next(new PageProcessor(_blogDomain, new ClientWrapper(), null)
                            .Next(new PageProcessor(_oldBlogDomain, new ClientWrapper(), null)
                            .Next(new PageProcessor(_websiteDomain, new ClientWrapper(), new LinksExtractor(_websiteDomain))
                            .Next(new UnknownProcessor())))))))));
        }

        public void AddUrl(string url)
        {
            urls.Add(url, new AwaitingProcessingUrlInfo(url));
        }

        public void Crawl()
        {
            var currentDepth = 0;
            while (urls.Where(x => x.Value is AwaitingProcessingUrlInfo).Count() > 0  && currentDepth < _maxDepth)
            {
                currentDepth++;

                var urlsToAdd = new List<IUrlInfo>();
                foreach (var key in urls.Where(x => x.Value is AwaitingProcessingUrlInfo).Select(x => x.Key))
                {
                    var info = ProcessUrl(key);

                    urlsToAdd.Add(info);
                }

                // Update the Url items
                foreach (var urlInfo in urlsToAdd)
                {
                    urls[urlInfo.Url] = urlInfo;
                }

                // Populate with any new links
                var newUrls = urlsToAdd.Where(x => x.HasLinks).SelectMany(x => x.Links).Distinct();

                var newUrlsToAdd = newUrls.Where(x => !urls.Keys.Contains(x.Url)).ToList();
                foreach (var newUrl in newUrls)
                {
                    if (!urls.Keys.Contains(newUrl.Url))
                    {
                        urls.Add(newUrl.Url, newUrl);
                    }
                }
            }

            CheckForProblems();
        }

        public bool Valid()
        {
            return _problems.Length == 0;
        }

        private void CheckForProblems()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var url in urls.Keys)
            {
                var urlInfo = urls[url];

                if (!urlInfo.Valid)
                {
                    builder.AppendLine(urlInfo.ToString());

                    var referencedIn = urls.Where(x => x.Value.HasLinks && x.Value.Links.Where(y => y.Url == urlInfo.Url).Count() > 0).Select(x => x.Key);
                    foreach (var reference in referencedIn)
                    {
                        builder.AppendFormat("\tReferenced in: {0}", reference);
                        builder.AppendLine();
                    }
                }

            }

            _problems = builder.ToString();
        }

        private IUrlInfo ProcessUrl(string url)
        {
            return _processor.Process(url);
        }

        public override string ToString()
        {
            return _problems;
        }
    }
}
