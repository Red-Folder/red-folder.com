using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RedFolder.ViewModels;
using System.IO;
using Newtonsoft.Json;

namespace RedFolder.Controllers.Web
{
    public class MyBioController : Controller
    {
        public IActionResult Index()
        {
            var employmentFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "employment.json");
            var employmentJson = System.IO.File.ReadAllText(employmentFilePath);
            var employment = JsonConvert.DeserializeObject<List<Employment>>(employmentJson);

            var certificationsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "certifications.json");
            var certificationsJson = System.IO.File.ReadAllText(certificationsFilePath);
            var certifications = JsonConvert.DeserializeObject<List<Certification>>(certificationsJson);

            var viewModel = new MyBio
            {
                Employment = employment.ToArray(),
                Certifications = certifications.ToArray()
            };

            return View(viewModel);
        }
    }
}
