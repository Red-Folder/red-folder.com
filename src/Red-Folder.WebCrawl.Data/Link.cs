using System.Runtime.Serialization;

namespace Red_Folder.WebCrawl.Data
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
