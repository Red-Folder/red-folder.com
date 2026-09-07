using System.Text.Json;

namespace RedFolder.Smoke;

/// <summary>Read-only public deployment checks with deliberately limited diagnostics.</summary>
public static class SmokeChecks
{
    private static readonly (string Path, int Status)[] Routes =
    [
        ("/health", 200), ("/", 200), ("/Blog", 200), ("/Podcasts", 200),
        ("/Projects", 200), ("/api/version", 200), ("/Activity", 410),
        ("/Activity/Weekly/2022/01", 410), ("/Activity/Books/2022", 410),
        ("/Activity/Skills/2022", 410)
    ];

    /// <summary>Checks each route, returning false on any failure. The client must have a finite timeout and redirects disabled.</summary>
    public static async Task<bool> RunAsync(HttpClient client, string expectedCommit, TextWriter output)
    {
        var passed = true;
        foreach (var (path, status) in Routes)
        {
            string? failure = null;
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, path);
                request.Headers.CacheControl = new() { NoCache = true };
                using var response = await client.SendAsync(request);
                if ((int)response.StatusCode != status)
                    failure = $"expected HTTP {status}, received {(int)response.StatusCode}";
                else if (path == "/Blog")
                {
                    var body = await response.Content.ReadAsStringAsync();
                    if (!body.Contains("<h1>Blog</h1>", StringComparison.Ordinal) ||
                        !body.Contains("class=\"blog-tiles row\"", StringComparison.Ordinal))
                        failure = "expected Blog page content missing";
                }
                else if (path is "/api/version" or "/health")
                {
                    using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                    var key = path == "/health" ? "status" : "commitSha";
                    var expected = path == "/health" ? "Healthy" : expectedCommit;
                    if (json.RootElement.ValueKind != JsonValueKind.Object ||
                        !json.RootElement.TryGetProperty(key, out var value) ||
                        value.ValueKind != JsonValueKind.String ||
                        !string.Equals(value.GetString(), expected, StringComparison.OrdinalIgnoreCase))
                        failure = path == "/health" ? "readiness is not Healthy" : "deployed commit mismatch or missing";
                }
            }
            catch (OperationCanceledException) { failure = "request timed out or cancelled"; }
            catch (HttpRequestException) { failure = "HTTP request failed"; }
            catch (JsonException) { failure = "invalid JSON"; }

            await output.WriteLineAsync(failure == null ? $"PASS {path}" : $"FAIL {path}: {failure}");
            passed &= failure == null;
        }
        return passed;
    }
}
