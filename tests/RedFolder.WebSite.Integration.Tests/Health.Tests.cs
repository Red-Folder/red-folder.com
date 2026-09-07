using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Moq;
using RedFolder.Controllers.Api;
using RedFolder.WebSite.Integration.Tests.Infrastructure;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests;

public class HealthTests
{
    [Theory]
    [InlineData(true, false, HttpStatusCode.OK, "Healthy")]
    [InlineData(false, false, HttpStatusCode.ServiceUnavailable, "Degraded")]
    [InlineData(true, true, HttpStatusCode.ServiceUnavailable, "Degraded")]
    public async Task Get_Health_ReportsOnlyReadiness(bool started, bool stopping,
        HttpStatusCode expectedStatus, string expectedBody)
    {
        var lifetime = new Mock<IHostApplicationLifetime>();
        lifetime.SetupGet(x => x.ApplicationStarted).Returns(new CancellationToken(started));
        lifetime.SetupGet(x => x.ApplicationStopping).Returns(new CancellationToken(stopping));
        using var host = await Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(web => web.UseTestServer()
                .UseEnvironment("Integration")
                .ConfigureAppConfiguration((_, config) => config.AddInMemoryCollection(
                    new Dictionary<string, string> { ["MediaRoot"] = null }))
                .UseStartup<IntegrationTestsStartup>()
                .ConfigureTestServices(services =>
                {
                    services.AddControllers().AddControllersAsServices();
                    services.AddTransient(_ => new HealthController(lifetime.Object));
                }))
            .StartAsync();
        using var client = host.GetTestClient();

        var response = await client.GetAsync("/health");

        Assert.Equal(expectedStatus, response.StatusCode);
        Assert.Equal("{\"status\":\"" + expectedBody + "\"}", await response.Content.ReadAsStringAsync());
        Assert.True(response.Headers.CacheControl.NoStore);
    }
}
