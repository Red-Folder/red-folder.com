using Microsoft.AspNetCore.Hosting;
using RedFolder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public class SiteMapRepository : ISiteMapRepository
    {
        private List<ISiteMapUrlRepository> _innerRepositories = new List<ISiteMapUrlRepository>();
        private string _baseUrl;

        public SiteMapRepository(IHostingEnvironment _env)
        {
            _baseUrl = "http://localhost:58352";

            if (_env.IsProduction())
            {
                _baseUrl = "https://www.red-folder.com";
            }

            if (_env.IsStaging())
            {
                _baseUrl = "http://rfc-website-staging.azurewebsites.net";
            }
        }

        public void AddRepository(ISiteMapUrlRepository inner)
        {
            _innerRepositories.Add(inner);
        }

        public SiteMap GetSiteMap()
        {
            return new SiteMap
            {
                Urls = GetUrls().Select(x => new SiteMapUrl { Location = _baseUrl + x }).ToList()
            };
        }

        public List<string> GetUrls()
        {
            var result = LocalUrls();

            foreach (var inner in _innerRepositories)
            {
                result.AddRange(inner.GetUrls());
            }

            return result;
        }

        private List<string> LocalUrls()
        {
            return new List<string>
            {
                "",
                "/Services",
                "/Projects",
                "/Projects/ROI",
                "/Projects/AspNetCore",
                "/Projects/Cordova",
                "/Projects/Cordova/BackgroundServicePlugin",
                "/Projects/Cordova/GPSServicePlugin",
                "/Projects/Cordova/SchedulerPlugin",
                "/Projects/Cordova/AvailabilityMonitorPlugin",
                "/Projects/Cordova/SMSHandlerPlugin",
                "/MyBio"
            };
        }
    }
}
