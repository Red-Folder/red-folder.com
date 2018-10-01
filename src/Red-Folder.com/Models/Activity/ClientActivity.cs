using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Models.Activity
{
    public class ClientActivity
    {
        [JsonProperty("clients")]
        public List<Client> Clients { get; set; }
    }
}
