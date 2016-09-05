using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Red_Folder.WebCrawl.Models;
using Red_Folder.WebCrawl.Helpers;

namespace Red_Folder.WebCrawl.Command
{
    public class PageProcessor : HttpClientBaseProcessor
    {
        private string _domain;
        private ILinksExtractor _linksExtrator;

        public PageProcessor(string domain, IClientWrapper httpClient, ILinksExtractor linksExtractor) : base(httpClient)
        {
            _domain = domain;
            _linksExtrator = linksExtractor;
        }

        public override IUrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Handle(url);
            }
            else
            {
                return base.Process(url);
            }
        }

        private bool CanBeHandled(string url)
        {
            return url.StartsWith(_domain);
        }

        private IUrlInfo Handle(string url)
        {
            HttpGet(url);
            if (LastHttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                var links = _linksExtrator.Extract(LastHttpResponse);
                if (links == null)
                {
                    return new PageUrlInfo(url);
                }
                else
                {
                    return new PageUrlInfo(url, links);
                }
            }
            else
            {
                return new PageUrlInfo(url, String.Format("Unexpected Status code: {0}", LastHttpStatusCode));
            }
        }
    }
}
