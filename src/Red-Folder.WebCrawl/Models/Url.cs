using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.WebCrawl.Models
{
    [DataContract]
    public class Url
    {
        [DataMember(Name ="path")]
        public string Path { get; private set; }

        [DataMember(Name = "valid")]
        public bool Valid { get; private set; }
        [DataMember(Name = "invalidationMessage")]
        public string InvalidationMessage { get; private set; }

        public Url(string path, bool valid, string invalidationMessage = "")
        {
            Path = path;
            Valid = valid;
            InvalidationMessage = invalidationMessage;
        }
    }
}
