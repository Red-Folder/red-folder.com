using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Red_Folder.Podcast;
using System.Net.Http;

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

            services.RemoveAll(typeof(IHttpClientFactory));
            services.AddSingleton<IHttpClientFactory, MockHttpClientFactory>();
        }
    }
}
