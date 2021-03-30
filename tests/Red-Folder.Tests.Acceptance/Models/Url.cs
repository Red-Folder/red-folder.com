using Newtonsoft.Json;

namespace Red_Folder.Tests.Acceptance.Models
{
    public class Url
    {
        [JsonProperty("Path")]
        public string Path { get; private set; }

        [JsonProperty("Valid")]
        public bool Valid { get; private set; }
        [JsonProperty("InvalidationMessage")]
        public string InvalidationMessage { get; private set; }
    }
}
