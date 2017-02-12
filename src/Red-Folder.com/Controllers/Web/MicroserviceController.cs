using Microsoft.AspNetCore.Mvc;
using RedFolder.Services;
using RedFolder.ViewModels;

namespace RedFolder.Controllers.Web
{
    public class MicroserviceController : Controller
    {
        // GET: MicroService
        public ActionResult Index([FromServices] IBlogRepository repository)
        {
            var blogs = repository.GetAll();
            var collection = new BlogCollection(blogs, 1, 0, "MicroServices", BlogCollection.OrderBy.PublishedAscending);
            return View(collection);
        }
    }
}