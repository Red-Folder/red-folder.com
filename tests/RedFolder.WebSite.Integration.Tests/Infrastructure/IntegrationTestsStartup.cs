using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Red_Folder.Podcast;

namespace RedFolder.WebSite.Integration.Tests.Infrastructure
{
    public class IntegrationTestsStartup : Startup
    {
        public IntegrationTestsStartup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment) : base(configuration, hostingEnvironment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddTransient<IFeedReader, MockFeedReader>();
        }
    }
}
