using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RedFolder.Controllers.Web
{
    public class SamplesController : Controller
    {
        public IActionResult WebCrawlGraph()
        {
            return View();
        }
    }
}