using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.Website.Data
{
    public class Blog
    {
        public string Url { get; set; }
        public string Author { get; set; }
        public DateTime Published { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }


        public Twitter Twitter { get; set; }
    }
}
