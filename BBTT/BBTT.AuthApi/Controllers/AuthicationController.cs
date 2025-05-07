using BBTT.AuthCore;
using Microsoft.AspNetCore.Mvc;
using UserModel;

namespace BBTT.AuthApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthicationController : ControllerBase
{
    
    private readonly ILogger<AuthicationController> _logger;
    private readonly IAuthAccesor _authAccesor;

    public AuthicationController(ILogger<AuthicationController> logger, IAuthAccesor authAccesor)
    {
        _logger = logger;
        _authAccesor = authAccesor ?? throw new ArgumentNullException(nameof(authAccesor));
    }

    [HttpPost("/CheckLogin",Name = "Login")]
    public async Task<ActionResult<User>> Login (LoginModel model)
    {
        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            return BadRequest("Email and password are required.");

        User temp = new();
        //var result = await _authAccesor.Login(email, password);
        //if (result == null)
        //    return NotFound();
        //return Ok(result);
        return Ok(temp);
    }

    [HttpGet]
    public async Task<ActionResult> get ()
    {
        return Ok();
    }
}
