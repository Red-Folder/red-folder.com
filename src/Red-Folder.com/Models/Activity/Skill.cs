using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Models.Activity
{
    public class Skill
    {
        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("totalDuration")]
        public long TotalDuration { get; set; }
    }
}
