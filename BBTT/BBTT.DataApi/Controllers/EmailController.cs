using BBTT.CrosswordModel;
using BBTT.DBModels;
using BBTT.DBModels.Crossword;
using BBTT.DBPostgres;
using BBTT.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using UserModel;

namespace BBTT.DataApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmail _email;
    private readonly IEmailDataAcess _emailDataAcess;

    public EmailController(IEmail email, IEmailDataAcess emailDataAcess)
    {
        _email = email ?? throw new ArgumentNullException(nameof(email));
        _emailDataAcess = emailDataAcess;
    }

    [HttpPost("{email}")]
    public async Task<IActionResult> SendEmailToUserForAuth(string email)
    {
        if (string.IsNullOrEmpty(email))
            return BadRequest("Email is required.");

        string code = RandomNumberGenerator.GetHexString(8);

        await _email.SendEmail(email, code);

        // Save the code to the database or cache for later verification
        await _emailDataAcess.SaveEmailCodeAsync(email, code);

        return Ok();
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyEmailCode (VerficationModel model)
    {
        string email = model.Email;
        string code = model.Code;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(code))
            return BadRequest("Email and code are required.");

        // Verify the code
        var isValid = await _emailDataAcess.VerifyEmailCodeAsync(email, code);
        if (!isValid)
            return BadRequest("Invalid code or is expired.");
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetStatus()
    {
        return Ok("Email service is running.");
    }
}