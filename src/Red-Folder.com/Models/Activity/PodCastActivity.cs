using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Red_Folder.com.Models.Activity
{
    public class PodCastActivity
    {
        [JsonProperty("categories")]
        public List<PodCastCategory> Categories;

        [JsonIgnore]
        public long TotalDuration
        {
            get
            {
                if (Categories == null || Categories.Count() == 0) return 0;

                var total = Categories.Aggregate((long)0, (acc, x) => acc + x.TotalDuration);

                return total;
            }
        }
    }
}
