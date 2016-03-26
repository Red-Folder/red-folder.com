using Microsoft.AspNet.Mvc;
using RedFolder.Services;

namespace RedFolder.Controllers.Web
{
    public class CordovaController : Controller
    {
        // GET: Cordova
        public ActionResult Index()
        {
            return View(CordovaProjectRepository.GetCordovaInfo());
        }

        public ActionResult BackgroundServicePlugin()
        {
            return View("Project", CordovaProjectRepository.GetBackgroundServicePluginInfo());
        }

        public ActionResult GPSServicePlugin()
        {
            return View("Project", CordovaProjectRepository.GetGPSServiceInfo());
        }

        public ActionResult SchedulerPlugin()
        {
            return View("Project", CordovaProjectRepository.GetSchedulerInfo());
        }

        public ActionResult AvailabilityMonitorPlugin()
        {
            return View("Project", CordovaProjectRepository.GetAvailabilityMonitorInfo());
        }

        public ActionResult SMSHandlerPlugin()
        {
            return View("Project", CordovaProjectRepository.GetSMSHandlerInfo());
        }

    }
}