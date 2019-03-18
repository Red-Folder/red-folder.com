using Newtonsoft.Json;
using System.Collections.Generic;

namespace Red_Folder.com.Models.Activity
{
    public class EventActivity: IActivity
    {
        [JsonProperty("events")]
        public List<Event> Events;
    }
}
