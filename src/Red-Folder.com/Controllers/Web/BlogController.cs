using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedFolder.Services;
using RedFolder.ViewModels;
using static RedFolder.ViewModels.BlogCollection;
using Microsoft.ApplicationInsights;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedFolder.Controllers.Web
{
    public class BlogController : Controller
    {
        private TelemetryClient telemetry = new TelemetryClient();

        // GET: /<controller>/
        public IActionResult Index([FromServices] IBlogRepository repository, 
                                    string url, 
                                    int pageNo = 1, 
                                    int blogsPerPage = 12, 
                                    string filterBy = null,
                                    OrderBy orderBy = OrderBy.PublishedDescending)
        {
            try
            {
                if (url == null || url.Length == 0)
                {
                    var blogs = repository.GetAll();
                    var collection = new BlogCollection(blogs, pageNo, blogsPerPage, filterBy, orderBy);
                    return View("BlogList", collection);
                }
                else
                {
                    var blog = repository.Get(url);
                    return View("Blog", blog);
                }
            }
            catch (BlogNotFoundException ex)
            {
                telemetry.TrackException(ex);
                return new RedirectResult("/errors/status/404");
            }
            catch (BlogNotEnabledException ex)
            {
                telemetry.TrackException(ex);
                return new RedirectResult("/errors/status/404");
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
                return new RedirectResult("/errors/status/500");
            }
        }
    }
}
