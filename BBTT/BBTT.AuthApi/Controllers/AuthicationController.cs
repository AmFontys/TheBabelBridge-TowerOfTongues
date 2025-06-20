using BBTT.AuthCore;
using Microsoft.AspNetCore.Mvc;
using UserModel;

namespace BBTT.AuthApi.Controllers;

[ApiController]
[Route("Auth")]
public class AuthicationController : ControllerBase
{
    private readonly ILogger<AuthicationController> _logger;
    private readonly IAuthAccesor _authAccesor;

    public AuthicationController(ILogger<AuthicationController> logger, IAuthAccesor authAccesor)
    {
        _logger = logger;
        _authAccesor = authAccesor ?? throw new ArgumentNullException(nameof(authAccesor));
    }

    [HttpGet("Authication")]
    public IActionResult TestConnection()
    {
        return Ok();
    }

    [HttpPost("CheckLogin")]
    public async Task<ActionResult<User>> Login([FromBody] LoginModel model)
    {
        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            return BadRequest("Email and password are required.");

        var result = await _authAccesor.Login(model.Email, model.Password);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register([FromBody] RegisterModel model)
    {
        var result = await _authAccesor.Register(model.Name, model.Email, model.Password);
        if (result == null)
            return BadRequest("Registration failed.");
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var result = await _authAccesor.Logout();
        if (!result)
            return BadRequest("Logout failed.");
        return Ok();
    }

    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
    {
        var result = await _authAccesor.ChangePassword(model.Email, model.OldPassword, model.NewPassword);
        if (!result)
            return BadRequest("Password change failed.");
        return Ok();
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
    {
        var result = await _authAccesor.ResetPassword(model.Email);
        if (!result)
            return BadRequest("Reset password failed.");
        return Ok();
    }

    [HttpPost("verifyEmail")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailModel model)
    {
        var result = await _authAccesor.VerifyEmail(model.Email);
        if (!result)
            return BadRequest("Email verification failed.");
        return Ok();
    }
}
