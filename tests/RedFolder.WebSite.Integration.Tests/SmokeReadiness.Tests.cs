using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RedFolder.Smoke;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests;

public class SmokeReadinessTests
{
    private const string Commit = "0123456789012345678901234567890123456789";

    [Fact]
    public async Task Wait_StartupFailuresAndStaleVersionRecover_ConfirmsExpectedDeployment()
    {
        var clock = new ManualTimeProvider();
        var attempts = 0;
        using var handler = new StartupHandler((request, _) =>
        {
            if (request.RequestUri.AbsolutePath == "/health")
            {
                attempts++;
                if (attempts == 1) throw new HttpRequestException("SECRET");
                if (attempts == 2) return Reply(HttpStatusCode.ServiceUnavailable, "SECRET");
                return Reply(HttpStatusCode.OK, "{\"status\":\"Healthy\"}");
            }
            return Reply(HttpStatusCode.OK, Version(attempts == 3 ? "stale" : Commit));
        });
        using var client = Client(handler);
        using var output = new StringWriter();

        var result = await clock.CompleteAsync(SmokeChecks.WaitForReadinessAsync(client, Commit, output,
            TimeSpan.FromSeconds(30), timeProvider: clock));

        Assert.True(result);
        Assert.Equal(TimeSpan.FromSeconds(15), clock.Elapsed);
        Assert.DoesNotContain("SECRET", output.ToString());
        Assert.All(handler.Paths, path => Assert.Contains(path, new[] { "/health", "/api/version" }));
    }

    [Theory]
    [InlineData("SECRET")]
    [InlineData("{}")]
    [InlineData("{\"commitSha\":null}")]
    [InlineData("{\"commitSha\":\"stale\"}")]
    public async Task Wait_VersionNeverMatches_FailsAtDeadline(string versionBody)
    {
        var clock = new ManualTimeProvider();
        using var handler = new StartupHandler((request, _) => Reply(HttpStatusCode.OK,
            request.RequestUri.AbsolutePath == "/health" ? "{\"status\":\"Healthy\"}" : versionBody));
        using var client = Client(handler);
        using var output = new StringWriter();

        var result = await clock.CompleteAsync(SmokeChecks.WaitForReadinessAsync(client, Commit, output,
            TimeSpan.FromSeconds(12), timeProvider: clock));

        Assert.False(result);
        Assert.Equal(TimeSpan.FromSeconds(12), clock.Elapsed);
        Assert.Equal(3, handler.Paths.Count(path => path == "/api/version"));
        Assert.DoesNotContain("SECRET", output.ToString());
    }

    [Theory]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    [InlineData(HttpStatusCode.Redirect)]
    public async Task Wait_HealthNeverReady_StopsWithoutRequestingVersion(HttpStatusCode status)
    {
        var clock = new ManualTimeProvider();
        using var handler = new StartupHandler((_, _) => Reply(status, "SECRET"));
        using var client = Client(handler);

        var result = await clock.CompleteAsync(SmokeChecks.WaitForReadinessAsync(client, Commit, TextWriter.Null,
            TimeSpan.FromSeconds(12), timeProvider: clock));

        Assert.False(result);
        Assert.Equal(new[] { "/health", "/health", "/health" }, handler.Paths);
        Assert.Equal(TimeSpan.FromSeconds(12), clock.Elapsed);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Wait_RequestStalls_OverallDeadlineCancelsRequest(bool retryBeforeStall)
    {
        var clock = new ManualTimeProvider();
        CancellationToken requestToken = default;
        var requests = 0;
        var stalled = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var handler = new StartupHandler(async (_, token) =>
        {
            if (requests++ == 0 && retryBeforeStall)
                return await Reply(HttpStatusCode.ServiceUnavailable, "");
            requestToken = token;
            stalled.SetResult(true);
            await Task.Delay(Timeout.Infinite, token);
            throw new InvalidOperationException("Unreachable");
        });
        using var client = Client(handler);

        var waiting = SmokeChecks.WaitForReadinessAsync(client, Commit, TextWriter.Null,
            TimeSpan.FromSeconds(12), timeProvider: clock);
        if (retryBeforeStall) clock.Advance(TimeSpan.FromSeconds(5));
        await stalled.Task;
        clock.Advance(TimeSpan.FromSeconds(retryBeforeStall ? 7 : 12));
        var result = await waiting;

        Assert.False(result);
        Assert.True(requestToken.IsCancellationRequested);
        Assert.Equal(TimeSpan.FromSeconds(12), clock.Elapsed);
        Assert.Equal(retryBeforeStall ? 2 : 1, handler.Paths.Count);
    }

    [Fact]
    public async Task Wait_CancelledDuringRetryDelay_StopsWithoutMoreRequests()
    {
        var clock = new ManualTimeProvider();
        using var cancellation = new CancellationTokenSource();
        using var handler = new StartupHandler((_, _) => Reply(HttpStatusCode.ServiceUnavailable, "SECRET"));
        using var client = Client(handler);
        using var output = new StringWriter();
        var waiting = SmokeChecks.WaitForReadinessAsync(client, Commit, output,
            TimeSpan.FromSeconds(30), cancellation.Token, clock);

        cancellation.Cancel();
        var result = await waiting;

        Assert.False(result);
        Assert.Single(handler.Paths);
        Assert.Contains("cancelled", output.ToString());
        Assert.Equal(TimeSpan.Zero, clock.Elapsed);
    }

    [Fact]
    public async Task Wait_Ready_FullSmokeStillChecksEachRouteOnceAndFailsBadPage()
    {
        using var handler = new StartupHandler((request, _) =>
        {
            var path = request.RequestUri.AbsolutePath;
            return path switch
            {
                "/health" => Reply(HttpStatusCode.OK, "{\"status\":\"Healthy\"}"),
                "/api/version" => Reply(HttpStatusCode.OK, Version(Commit)),
                "/" => Reply(HttpStatusCode.InternalServerError, "SECRET"),
                "/Blog" => Reply(HttpStatusCode.OK, "<h1>Blog</h1><div class=\"blog-tiles row\"></div>"),
                _ => Reply(path.StartsWith("/Activity") ? HttpStatusCode.Gone : HttpStatusCode.OK, "")
            };
        });
        using var client = Client(handler);

        Assert.True(await SmokeChecks.WaitForReadinessAsync(client, Commit, TextWriter.Null, TimeSpan.FromSeconds(30)));
        Assert.False(await SmokeChecks.RunAsync(client, Commit, TextWriter.Null));
        Assert.Equal(1, handler.Paths.Count(path => path == "/"));
        Assert.Equal(12, handler.Paths.Count);
    }

    private static string Version(string commit) => "{\"commitSha\":\"" + commit + "\"}";
    private static HttpClient Client(HttpMessageHandler handler) => new(handler)
    {
        BaseAddress = new Uri("https://example.test"),
        Timeout = TimeSpan.FromSeconds(120)
    };
    private static Task<HttpResponseMessage> Reply(HttpStatusCode status, string body) =>
        Task.FromResult(new HttpResponseMessage(status) { Content = new StringContent(body) });

    private sealed class StartupHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _respond;
        public List<string> Paths { get; } = new();
        public StartupHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> respond) => _respond = respond;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.True(request.Headers.CacheControl.NoCache);
            Paths.Add(request.RequestUri.AbsolutePath);
            return _respond(request, cancellationToken);
        }
    }

    // Drives the same TimeProvider timers used by the overall deadline and retry delay.
    // No real waiting or additional test dependency is needed.
    private sealed class ManualTimeProvider : TimeProvider
    {
        private readonly object _gate = new();
        private readonly List<ManualTimer> _timers = new();
        private TaskCompletionSource<bool> _scheduled = Signal();
        private long _ticks;
        public override long TimestampFrequency => TimeSpan.TicksPerSecond;
        public TimeSpan Elapsed => TimeSpan.FromTicks(GetTimestamp());
        public override long GetTimestamp() { lock (_gate) return _ticks; }
        private static TaskCompletionSource<bool> Signal() => new(TaskCreationOptions.RunContinuationsAsynchronously);

        public override ITimer CreateTimer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
        {
            var timer = new ManualTimer(this, callback, state);
            lock (_gate) _timers.Add(timer);
            timer.Change(dueTime, period);
            return timer;
        }

        public void Advance(TimeSpan amount)
        {
            ManualTimer[] due;
            lock (_gate)
            {
                _ticks += amount.Ticks;
                due = _timers.Where(timer => timer.Due.HasValue && timer.Due <= _ticks).ToArray();
                foreach (var timer in due) timer.Due = null;
            }
            foreach (var timer in due) timer.Fire();
        }

        public async Task<bool> CompleteAsync(Task<bool> operation)
        {
            for (var iterations = 0; !operation.IsCompleted; iterations++)
            {
                Assert.True(iterations < 100, "Readiness wait did not converge or observe its deadline.");
                Task nextSchedule;
                ManualTimer[] due;
                lock (_gate)
                {
                    nextSchedule = _scheduled.Task;
                    var next = _timers.Where(timer => timer.Due.HasValue).Min(timer => timer.Due.Value);
                    _ticks = next;
                    due = _timers.Where(timer => timer.Due == next).ToArray();
                    foreach (var timer in due) timer.Due = null;
                }
                foreach (var timer in due) timer.Fire();
                await Task.WhenAny(operation, nextSchedule);
            }
            return await operation;
        }

        private sealed class ManualTimer : ITimer
        {
            private readonly ManualTimeProvider _clock;
            private readonly TimerCallback _callback;
            private readonly object _state;
            private bool _disposed;
            public long? Due { get; set; }
            public ManualTimer(ManualTimeProvider clock, TimerCallback callback, object state)
                => (_clock, _callback, _state) = (clock, callback, state);
            public bool Change(TimeSpan dueTime, TimeSpan period)
            {
                lock (_clock._gate)
                {
                    if (_disposed) return false;
                    Assert.Equal(Timeout.InfiniteTimeSpan, period);
                    Due = dueTime == Timeout.InfiniteTimeSpan ? null : _clock._ticks + dueTime.Ticks;
                    var signal = _clock._scheduled;
                    _clock._scheduled = Signal();
                    signal.TrySetResult(true);
                    return true;
                }
            }
            public void Fire() { if (!_disposed) _callback(_state); }
            public void Dispose() { lock (_clock._gate) { _disposed = true; Due = null; } }
            public ValueTask DisposeAsync() { Dispose(); return ValueTask.CompletedTask; }
        }
    }
}
