using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace RedFolder.WebSite.Integration.Tests
{
    public class HttpClientFixture : IDisposable
    {
        private readonly IHost _host;
        public HttpClient Client { get; set; }

        public HttpClientFixture()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var webRootPath = Path.Combine(projectDir, "../../../../../src/Red-Folder.com/wwwroot");

            _host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .UseContentRoot(projectDir)
                        .ConfigureAppConfiguration((context, config) =>
                        {
                            var integrationConfiguration = new Dictionary<string, string>
                            {
                            };
                            config.AddInMemoryCollection(integrationConfiguration);
                        })
                        .UseWebRoot(webRootPath)
                        .UseStartup<IntegrationTestsStartup>()
                        .UseEnvironment("Development");
                })
                .Start();

            Client = _host.GetTestServer().CreateClient();
        }

        public void Dispose()
        {
            Client?.Dispose();
            _host.StopAsync().Wait();
        }
    }
}
