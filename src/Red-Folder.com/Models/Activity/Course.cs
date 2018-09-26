using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Models.Activity
{
    public class Course
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("percentageComplete")]
        public int PercentageComplete { get; set; }
        [JsonProperty("courseImageUrl")]
        public string CourseImageUrl { get; set; }
    }
}
