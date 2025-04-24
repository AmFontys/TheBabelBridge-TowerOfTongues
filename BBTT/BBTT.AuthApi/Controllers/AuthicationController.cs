using Microsoft.AspNetCore.Mvc;

namespace BBTT.AuthApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthicationController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<AuthicationController> _logger;

    public AuthicationController(ILogger<AuthicationController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Login")]
    public IActionResult Login ()
    {

    }
}
