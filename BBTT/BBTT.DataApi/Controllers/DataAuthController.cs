using BBTT.AuthCore;
using BBTT.DBPostgres;
using Microsoft.AspNetCore.Mvc;
using UserModel;

namespace BBTT.DataApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DataAuthController : ControllerBase
{     
    private readonly IUserDataAcess _userDataAccess;
    private readonly EncryptionConfig encryptionConfig = new();

    public DataAuthController (IUserDataAcess userDataAccess)
    {
        _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
    }
        
    [HttpGet("/A")]
    public async Task<IActionResult> testGet ()
    {
        
        return Ok();
    }

    [ HttpPost("/login")]
    public async Task<IActionResult> Login (LoginModel model)
    {
        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            return BadRequest("Email and password are required.");
        var result = await _userDataAccess.FindUsersByEmail(model.Email);
        if (result == null || result.Count<1)
            return NotFound();
        // Check if the password is correct
        var saltedPassword = encryptionConfig.Hash(model.Password + result.FirstOrDefault().Salt);

        var gottenUser = await _userDataAccess.GetUser(model.Email, saltedPassword);
        if (gottenUser == null)
            return NotFound();

        return Ok(gottenUser);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register (RegisterModel model)
    {
        if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            return BadRequest("Name, email and password are required.");
        var salt = encryptionConfig.Salt();
        var saltedPassword = encryptionConfig.Hash(model.Password + salt);
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Password = saltedPassword,
            Role = UserRoles.User,
            Salt = salt
        };
        var result = await _userDataAccess.CreateUser(user);
        if (result == null)
            return NotFound();
        return Ok(result);
    }


}
