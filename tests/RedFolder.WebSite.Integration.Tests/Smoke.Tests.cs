using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RedFolder.Smoke;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests;

public class SmokeTests
{
    private const string Commit = "0123456789012345678901234567890123456789";

    [Theory]
    [InlineData("success", true)]
    [InlineData("status", false)]
    [InlineData("redirect", false)]
    [InlineData("commit", false)]
    [InlineData("json", false)]
    [InlineData("missing", false)]
    [InlineData("blog", false)]
    [InlineData("degraded", false)]
    [InlineData("retired", false)]
    [InlineData("timeout", false)]
    [InlineData("network", false)]
    public async Task Run_DeploymentResponse_ReturnsExpectedResultWithoutSensitiveDiagnostics(string scenario, bool expected)
    {
        using var client = new HttpClient(new DeploymentHandler(scenario))
        {
            BaseAddress = new Uri("https://example.test"),
            Timeout = TimeSpan.FromMilliseconds(100)
        };
        using var output = new StringWriter();

        var result = await SmokeChecks.RunAsync(client, Commit, output);

        Assert.Equal(expected, result);
        Assert.DoesNotContain("SECRET", output.ToString());
        Assert.Equal(10, output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Length);
    }

    private sealed class DeploymentHandler : HttpMessageHandler
    {
        private readonly string _scenario;
        public DeploymentHandler(string scenario) => _scenario = scenario;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            var path = request.RequestUri.AbsolutePath;
            var status = path.StartsWith("/Activity") ? HttpStatusCode.Gone : HttpStatusCode.OK;
            var body = path switch
            {
                "/health" => "{\"status\":\"Healthy\"}",
                "/Blog" => "<h1>Blog</h1><div class=\"blog-tiles row\"></div>",
                "/api/version" => "{\"commitSha\":\"" + Commit + "\"}",
                _ => "SECRET"
            };
            if (path == "/")
            {
                if (_scenario == "timeout") await Task.Delay(Timeout.Infinite, cancellationToken);
                if (_scenario == "network") throw new HttpRequestException("SECRET");
                if (_scenario == "status") status = HttpStatusCode.InternalServerError;
                if (_scenario == "redirect") status = HttpStatusCode.Redirect;
            }
            if (path == "/api/version")
                body = _scenario switch
                {
                    "commit" => "{\"commitSha\":\"SECRET\"}",
                    "json" => "SECRET",
                    "missing" => "{}",
                    _ => body
                };
            if (path == "/Blog" && _scenario == "blog") body = "SECRET";
            if (path == "/health" && _scenario == "degraded") body = "{\"status\":\"Degraded\"}";
            if (path.StartsWith("/Activity") && _scenario == "retired") status = HttpStatusCode.InternalServerError;
            return new HttpResponseMessage(status) { Content = new StringContent(body) };
        }
    }
}
