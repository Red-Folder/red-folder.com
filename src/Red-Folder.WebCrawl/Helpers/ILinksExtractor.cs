using Red_Folder.WebCrawl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Helpers
{
    public interface ILinksExtractor
    {
        IList<IUrlInfo> Extract(string content);
    }
}
