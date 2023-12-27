using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace RedFolder.WebSite.Integration.Tests
{
    public class IntegrationTestsStartup : Startup
    {
        public IntegrationTestsStartup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment) : base(configuration, hostingEnvironment)
        {
        }
    }
}
