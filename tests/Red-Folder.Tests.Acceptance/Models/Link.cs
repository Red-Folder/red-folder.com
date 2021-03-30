using Newtonsoft.Json;

namespace Red_Folder.Tests.Acceptance.Models
{
    public class Link
    {
        [JsonProperty("Source")]

        public string Source { get; private set; }
        [JsonProperty("Target")]
        public string Target { get; private set; }
    }
}
