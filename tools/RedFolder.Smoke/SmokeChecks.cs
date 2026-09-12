using System.Text.Json;

namespace RedFolder.Smoke;

/// <summary>Read-only public deployment checks with deliberately limited diagnostics.</summary>
public static class SmokeChecks
{
    /// <summary>
    /// Waits only for host readiness and the expected deployment version. The overall
    /// deadline also bounds each request and response body read. Full smoke checks
    /// must still run once after this succeeds. Redirects must be disabled by the caller.
    /// </summary>
    public static async Task<bool> WaitForReadinessAsync(HttpClient client, string expectedCommit,
        TextWriter output, TimeSpan startupWait, CancellationToken cancellationToken = default,
        TimeProvider? timeProvider = null)
    {
        if (startupWait <= TimeSpan.Zero || startupWait > TimeSpan.FromSeconds(300))
            throw new ArgumentOutOfRangeException(nameof(startupWait));

        var clock = timeProvider ?? TimeProvider.System;
        var started = clock.GetTimestamp();
        using var deadline = new CancellationTokenSource(startupWait, clock);
        using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, deadline.Token);
        var token = cancellation.Token;

        try
        {
            while (!token.IsCancellationRequested && clock.GetElapsedTime(started) < startupWait)
            {
                if (await HasExpectedValueAsync(client, "/health", "status", "Healthy", token) &&
                    await HasExpectedValueAsync(client, "/api/version", "commitSha", expectedCommit, token) &&
                    !token.IsCancellationRequested && clock.GetElapsedTime(started) < startupWait)
                {
                    await output.WriteLineAsync("PASS startup: readiness and deployed commit confirmed");
                    return true;
                }

                var remaining = startupWait - clock.GetElapsedTime(started);
                if (remaining <= TimeSpan.Zero) break;
                await Task.Delay(remaining < TimeSpan.FromSeconds(5) ? remaining : TimeSpan.FromSeconds(5), clock, token);
            }
        }
        catch (OperationCanceledException) { }

        await output.WriteLineAsync(cancellationToken.IsCancellationRequested
            ? "FAIL startup: readiness wait cancelled"
            : "FAIL startup: readiness or expected deployed commit not confirmed before deadline");
        return false;
    }

    private static async Task<bool> HasExpectedValueAsync(HttpClient client, string path,
        string key, string expected, CancellationToken cancellationToken)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.CacheControl = new() { NoCache = true };
            using var response = await client.SendAsync(request, cancellationToken);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) return false;
            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync(cancellationToken));
            return json.RootElement.ValueKind == JsonValueKind.Object &&
                json.RootElement.TryGetProperty(key, out var value) &&
                value.ValueKind == JsonValueKind.String &&
                string.Equals(value.GetString(), expected, StringComparison.OrdinalIgnoreCase);
        }
        catch (HttpRequestException) { return false; }
        catch (JsonException) { return false; }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested) { return false; }
    }

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
