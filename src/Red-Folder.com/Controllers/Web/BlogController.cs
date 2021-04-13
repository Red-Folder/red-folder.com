using System;
using Microsoft.AspNetCore.Mvc;
using RedFolder.Services;
using RedFolder.ViewModels;
using static RedFolder.ViewModels.BlogCollection;
using Microsoft.ApplicationInsights;
using RedFolder.com.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedFolder.Controllers.Web
{
    public class BlogController : Controller
    {
        private readonly TelemetryClient _telemetry;

        public BlogController(TelemetryClient telemetry)
        {
            _telemetry = telemetry;
        }

        // GET: /<controller>/
        public IActionResult Index([FromServices] IBlogRepository repository, 
                                    string url, 
                                    int pageNo = 1, 
                                    int blogsPerPage = 12, 
                                    string filterBy = null,
                                    OrderBy orderBy = OrderBy.PublishedDescending,
                                    bool reload = false)
        {
            try
            {
                if (reload) repository.Reload();

                if (url == null || url.Length == 0)
                {
                    var blogs = repository.GetAll();
                    var collection = new BlogCollection(blogs, pageNo, blogsPerPage, filterBy, orderBy);
                    return View("BlogList", collection);
                }
                else
                {
                    var meta = repository.Get(url);
                    var content = repository.LoadContent(meta);
                    var blog = new BlogWithContent
                    {
                        Meta = meta,
                        Content = content
                    };
                    return View("Blog", blog);
                }
            }
            catch (BlogNotFoundException ex)
            {
                _telemetry.TrackException(ex);
                return new RedirectResult("/errors/status/404");
            }
            catch (BlogNotEnabledException ex)
            {
                _telemetry.TrackException(ex);
                return new RedirectResult("/errors/status/404");
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return new RedirectResult("/errors/status/500");
            }
        }
    }
}
