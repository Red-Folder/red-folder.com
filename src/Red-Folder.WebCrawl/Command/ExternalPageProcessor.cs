using Red_Folder.WebCrawl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Command
{
    public class ExternalPageProcessor : BaseProcessor
    {
        private IList<string> _internalDomains;

        public ExternalPageProcessor(IList<string> internalDomains)
        {
            _internalDomains = internalDomains;
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
            if (_internalDomains.Where(x => url.StartsWith(x)).Count() > 0) return false;

            return true;
        }

        private IUrlInfo Handle(string url)
        {
            return new ExternalPageUrlInfo(url);
        }

        private IList<string> PopulateInternalDomains()
        {
            return new List<string>
            {
                //@"https://github.com/red-folder",
                //@"http://red-folder.blogspot.co.uk/",

            };
        }
    }
}
