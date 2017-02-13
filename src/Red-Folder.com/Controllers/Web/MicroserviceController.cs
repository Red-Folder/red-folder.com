using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using RedFolder.Services;
using RedFolder.ViewModels;

namespace RedFolder.Controllers.Web
{
    public class MicroserviceController : Controller
    {
        private TelemetryClient telemetry = new TelemetryClient();

        // GET: MicroService
        public ActionResult Index([FromServices] IBlogRepository repository)
        {
            telemetry.TrackEvent("MicoService page");
            var blogs = repository.GetAll();
            telemetry.TrackTrace("Retrieved all blogs");
            var collection = new BlogCollection(blogs, 1, 0, "MicroServices", BlogCollection.OrderBy.PublishedAscending);
            telemetry.TrackTrace("Produced the Blog Collection");
            return View(collection);
        }
    }
}