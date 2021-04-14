using Microsoft.AspNetCore.Mvc;
using RedFolder.Services;

namespace RedFolder.Controllers.Api
{
    [Produces("application/xml")]
    [Route("sitemap.xml")]
    public class SiteMapController : Controller
    {
        [HttpGet]
        public IActionResult Get([FromServices]ISiteMapRepository repo)
        {
            return Ok(repo.GetSiteMap());
        }
    }
}