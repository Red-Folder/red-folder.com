using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RedFolder.Blog;
using RedFolder.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RedFolder.Services
{
    public class SiteMapRepository : ISiteMapRepository
    {
        private List<ISiteMapUrlRepository> _innerRepositories;
        private string _baseUrl;

        public SiteMapRepository(IWebHostEnvironment _env, IEnumerable<ISiteMapUrlRepository> innerRepositories)
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

            _innerRepositories = innerRepositories.ToList();
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
                "/MyBio"
            };
        }
    }
}
