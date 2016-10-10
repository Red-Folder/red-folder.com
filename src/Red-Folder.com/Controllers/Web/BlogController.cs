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
        public IActionResult Index([FromServices] IBlogRepository repository, string title)
        {
            var blog = repository.Get(title);
            return View(blog);
        }
    }
}
