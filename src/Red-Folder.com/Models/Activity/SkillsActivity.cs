using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Models.Activity
{
    public class SkillsActivity
    {
    [JsonProperty("skills")]
    public List<Skill> Skills;

    [JsonIgnore]
    public long TotalDuration
    {
        get
        {
            if (Skills == null || Skills.Count() == 0) return 0;

            return Skills.Aggregate((long)0, (acc, x) => acc + x.TotalDuration);
        }
    }
}
}
