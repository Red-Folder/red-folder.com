using System.Runtime.Serialization;

namespace Red_Folder.WebCrawl.Data
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
