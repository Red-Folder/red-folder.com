using Microsoft.AspNetCore.Mvc;

namespace Red_Folder.com.Controllers.Web;

/// <summary>Marks the retired Activity area as permanently unavailable.</summary>
public class ActivityController : Controller
{
    /// <summary>Returns Gone without contacting the retired Activity service.</summary>
    [HttpGet("/Activity")]
    [HttpGet("/Activity/{**path}")]
    public IActionResult Gone() => new ContentResult
    {
        StatusCode = 410,
        ContentType = "text/plain",
        Content = "Gone"
    };
}
