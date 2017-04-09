using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace RedFolder.Controllers.Web
{
    public class ErrorsController : Controller
    {
        public IActionResult Status(int id)
        {
            var originalRequest = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (originalRequest != null)
            {
                if (originalRequest is StatusCodeReExecuteFeature)
                {
                    var pathBase = (originalRequest as StatusCodeReExecuteFeature).OriginalPathBase;
                    var path = (originalRequest as StatusCodeReExecuteFeature).OriginalPath;
                    var queryString = (originalRequest as StatusCodeReExecuteFeature).OriginalQueryString;

                    // Redirect Actions
                    if (id == 404 && path != null && path.Length > 0)
                    {
                        if (path.ToLower().StartsWith("/home/recentprojects")) return RedirectToActionPermanent("index", "projects");
                    }
                }
            }

            if (id == 500 || id == 404)
            {
                return View(string.Format("Status-{0}", id));
            } 
            else
            {
                return View();
            }
        }
    }
}