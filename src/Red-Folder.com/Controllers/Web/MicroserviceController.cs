using Microsoft.AspNetCore.Mvc;

namespace RedFolder.Controllers.Web
{
    public class MicroserviceController : Controller
    {
        // GET: MicroService
        public ActionResult Index()
        {
            return View();
        }
    }
}