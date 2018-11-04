using Newtonsoft.Json;
using System.Collections.Generic;

namespace Red_Folder.com.Models.Activity
{
    public class BlogActivity: IActivity
    {
        [JsonProperty("blogs")]
        public List<Blog> Blogs { get; set; }
    }
}
