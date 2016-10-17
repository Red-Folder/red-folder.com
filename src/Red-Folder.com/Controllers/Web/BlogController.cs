using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedFolder.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedFolder.Controllers.Web
{
    public class BlogController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index([FromServices] IBlogRepository repository, string url)
        {
            try
            {
                if (url == null || url.Length == 0)
                {
                    var blogs = repository.GetAll();
                    return View("BlogList", blogs);
                }
                else
                {
                    var blog = repository.Get(url);
                    return View("Blog", blog);
                }
            }
            catch (BlogNotFoundException ex)
            {
                return new RedirectResult("/errors/status/404");
            }
            catch (BlogNotEnabledException ex)
            {
                return new RedirectResult("/errors/status/404");
            }
            catch (Exception ex)
            {
                return new RedirectResult("/errors/status/500");
            }
        }
    }
}
