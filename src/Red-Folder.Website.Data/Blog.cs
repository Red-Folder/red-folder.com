using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Website.Data
{
    public class Blog
    {
        public string Url { get; set; }
        public string Author { get; set; }
        public DateTime Published { get; set; }
        public DateTime Modified { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string TwitterHandle { get; set; }

        public bool Enabled { get; set; }
    }
}
