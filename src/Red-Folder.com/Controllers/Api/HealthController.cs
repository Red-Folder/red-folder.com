using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace RedFolder.Controllers.Api;

/// <summary>Reports application readiness without inspecting external dependencies.</summary>
[ApiController]
public class HealthController : ControllerBase
{
    private readonly IHostApplicationLifetime _lifetime;

    public HealthController(IHostApplicationLifetime lifetime) => _lifetime = lifetime;

    /// <summary>Returns only readiness; starting or stopping is degraded.</summary>
    [HttpGet("/health")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public IActionResult Get()
    {
        var ready = _lifetime.ApplicationStarted.IsCancellationRequested &&
            !_lifetime.ApplicationStopping.IsCancellationRequested;
        return StatusCode(ready ? 200 : 503, new { status = ready ? "Healthy" : "Degraded" });
    }
}
