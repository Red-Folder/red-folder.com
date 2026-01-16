using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RedFolder.Models;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedFolder.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IHostEnvironment _environment;
        private readonly ILogger<VersionController> _logger;

        public VersionController(IHostEnvironment environment, ILogger<VersionController> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        /// <summary>
        /// Gets version and build information for the deployed application
        /// </summary>
        /// <returns>Version information including commit SHA, build time, and branch</returns>
        [HttpGet]
        public async Task<ActionResult<VersionInfo>> Get()
        {
            var versionFilePath = Path.Combine(_environment.ContentRootPath, "version.json");
            
            if (!System.IO.File.Exists(versionFilePath))
            {
                return NotFound(new { message = "Version information not available" });
            }

            try
            {
                var json = await System.IO.File.ReadAllTextAsync(versionFilePath);
                var versionInfo = JsonSerializer.Deserialize<VersionInfo>(json, new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true 
                });
                
                return Ok(versionInfo);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing version.json");
                return StatusCode(500, new { message = "Error reading version information" });
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "Error reading version.json file");
                return StatusCode(500, new { message = "Error reading version information" });
            }
        }
    }
}
