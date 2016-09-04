using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Helpers
{
    public class LinksExtractor
    {
        private IClientWrapper _client;
        private string _prefix = "https://www.red-folder.com";

        private IList<string> patterns = new List<string>();

        public LinksExtractor(IClientWrapper client)
        {
            _client = client;

            // Inlcude spaces to avoid JavaScript object setup
            patterns.Add(@" src(\s*)=(\s*)(""|')(?<url>.*?)(""|')");
            patterns.Add(@" href(\s*)=(\s*)(""|')(?<url>.*?)(""|')");
            patterns.Add(@"<loc>(?<url>.*?)</loc>");
        }

        public List<string> Extract()
        {
            var links = new List<string>();

            var content = _client.GetLastResponse().ToLower();

            foreach (var pattern in patterns)
            {
                foreach (Match match in Regex.Matches(content, pattern))
                {
                    links.Add(Format(match.Groups["url"].Value));
                }
            }

            return links;
        }

        private string Format(string url)
        {
            return EnsurePrefixed(url.ToLower());
        }

        private string EnsurePrefixed(string url)
        {
            if (url.ToLower().StartsWith("http"))
            {
                return url;
            }
            else
            {
                return _prefix + url;
            }
        }
    }
}
