using Newtonsoft.Json;

namespace Red_Folder.com.Models.Activity
{
    public class Book
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
