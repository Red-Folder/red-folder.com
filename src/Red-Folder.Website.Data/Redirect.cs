using System.Net;

namespace RedFolder.Website.Data
{
    public class Redirect
    {
        public HttpStatusCode RedirectType { get; set; }
        public string Url { get; set; }
        public bool RedirectByRoute { get; set; }
        public bool RedirectByParameter { get; set; }
    }
}
