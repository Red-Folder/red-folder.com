using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Models.Activity
{
    public class PluralsightActivity
    {
        [JsonProperty("courses")]
        public List<Course> Courses;
    }
}
