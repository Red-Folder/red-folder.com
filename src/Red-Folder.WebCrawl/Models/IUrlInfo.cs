using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    public interface IUrlInfo
    {
        string Url { get; }
        bool Valid { get; }
        string InvalidationMessage { get; }
        bool HasLinks { get; }
        IList<IUrlInfo> Links { get; }
    }
}
