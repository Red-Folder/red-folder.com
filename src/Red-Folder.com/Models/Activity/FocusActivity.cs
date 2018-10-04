using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Models.Activity
{
    public class FocusActivity: IActivity
    {
        [JsonProperty("focus")]
        public List<Skill> Focus;

        [JsonIgnore]
        public long TotalDuration
        {
            get
            {
                if (Focus == null || Focus.Count() == 0) return 0;

                return Focus.Aggregate((long)0, (acc, x) => acc + x.TotalDuration);
            }
        }
    }
}
