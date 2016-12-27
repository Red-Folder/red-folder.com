using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedFolder.ViewModels;
using RedFolder.Services;

namespace RedFolder.Controllers.Api
{
    [Produces("application/xml")]
    [Route("sitemap.xml")]
    public class SiteMapController : Controller
    {
        public SiteMap Get([FromServices]ISiteMapRepository repo)
        {
            return repo.GetSiteMap();
        }

    }
}