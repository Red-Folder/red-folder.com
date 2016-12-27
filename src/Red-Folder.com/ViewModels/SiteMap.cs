
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RedFolder.ViewModels
{
    [XmlRoot(ElementName = "urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class SiteMap
    {
        [XmlElement(ElementName = "url")]
        public List<SiteMapUrl> Urls { get; set; }
    }
}
