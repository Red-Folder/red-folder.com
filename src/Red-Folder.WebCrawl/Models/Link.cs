using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    [DataContract]
    public class Link
    {
        [DataMember(Name= "source")]
        public string Source { get; private set; }
        [DataMember(Name = "target")]
        public string Target { get; private set; }

        public Link(string source, string target)
        {
            Source = source;
            Target = target;
        }
    }
}
