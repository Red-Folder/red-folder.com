using Red_Folder.WebCrawl.Helpers;
using Red_Folder.WebCrawl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Command
{
    public interface IProcessUrl
    {
        IUrlInfo Process(string url);
        IProcessUrl Next(IProcessUrl nextProcessor);
    }
}
