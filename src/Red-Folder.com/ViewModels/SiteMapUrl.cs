using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RedFolder.ViewModels
{
    
    public class SiteMapUrl
    {
        [XmlElement(ElementName = "loc")]
        public string Location { get; set; }
    }
}
