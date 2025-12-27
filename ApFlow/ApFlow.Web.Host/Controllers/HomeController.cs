using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace ApFlow.Web.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : AbpControllerBase
{
    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        return "ApFlow.Web.Host is running!";
    }

    [HttpGet("version")]
    public async Task<ActionResult<Dictionary<string, string>>> GetVersion()
    {
        return new Dictionary<string, string>
        {
            { "Application", "ApFlow" },
            { "Version", "1.0.0" },
            { "Framework", "ABP vNext 8.3.4" }
        };
    }
}