
using System.Net.Http.Json;
using System.Net.Security;
using UserModel;

namespace BBTT.AuthCore;

public class AuthAccesor : IAuthAccesor
{
    private readonly HttpClient _httpClient;

    public AuthAccesor(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // Do NOT set BaseAddress here; configure it in DI to point to the API gateway.
        _httpClient.Timeout = TimeSpan.FromMinutes(5);
    }

    public async Task<User> Login(string email, string password)
    {
        var request = await _httpClient.PostAsJsonAsync("/data/login", new { email, password });
        request.EnsureSuccessStatusCode();
        User? result = await request.Content.ReadFromJsonAsync<User>();
        return result!;
    }

    public Task<bool> ChangePassword(string email, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetUserInfo(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Logout()
    {
        throw new NotImplementedException();
    }

    public Task<User> Register(string name, string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetPassword(string email)
    {
        throw new NotImplementedException();
    }

    public string SaltPassword(string password)
    {
        EncryptionConfig config = new EncryptionConfig();
        string salt = config.Salt();
        string saltedPassword = password + salt;
        string hashedPassword = config.Hash(saltedPassword);
        return hashedPassword;
    }

    public Task<bool> VerifyEmail(string email)
    {
        throw new NotImplementedException();
    }
}
