using Microsoft.AspNetCore.Mvc;
using RedFolder.ViewModels;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RedFolder.Controllers.Web
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            var servicesFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "services.json");
            var servicesJson = System.IO.File.ReadAllText(servicesFilePath);
            var services = JsonConvert.DeserializeObject<List<Service>>(servicesJson);
            return View(services);
        }
    }
}