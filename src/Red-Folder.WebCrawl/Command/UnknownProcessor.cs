using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Red_Folder.WebCrawl.Models;

namespace Red_Folder.WebCrawl.Command
{
    public class UnknownProcessor : BaseProcessor
    {
        public override IUrlInfo Process(string url)
        {
            return new UnknownUrlInfo(url);
        }
    }
}
