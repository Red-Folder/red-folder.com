﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Models.Activity
{
    public class Course
    {
        [JsonProperty("courseId")]
        public string CourseId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("percentageComplete")]
        public int PercentageComplete { get; set; }
        [JsonProperty("courseImageUrl")]
        public string CourseImageUrl { get; set; }
        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
