using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Red_Folder.WebCrawl.Models;

namespace Red_Folder.WebCrawl.Command
{
    public partial class BaseProcessor : IProcessUrl
    {
        private IProcessUrl _next = null;

        public IProcessUrl Next(IProcessUrl nextProcessor)
        {
            _next = nextProcessor;
            return this;
        }

        public virtual IUrlInfo Process(string url)
        {
            if (_next == null)
            {
                return null;
            }
            else
            {
                return _next.Process(url);
            }
        }
    }
}
