
using System.Net.Http.Json;
using System.Net.Security;
using UserModel;

namespace BBTT.AuthCore;

public class AuthAccesor : IAuthAccesor
{
    public HttpClient _httpClient;

    public AuthAccesor (HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:7086");
        _httpClient.Timeout = TimeSpan.FromMinutes(5);
    }

    public Task<bool> ChangePassword (string email, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetUserInfo (string email)
    {
        throw new NotImplementedException();
    }

    public async Task<User> Login (string email, string password)
    {
        var request = await _httpClient.PostAsJsonAsync("/DataAuth", new { email, password });
        request.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
        User? result = await request.Content.ReadFromJsonAsync<User>();
        return result;
    }

    public Task<bool> Logout ()
    {
        throw new NotImplementedException();
    }

    public Task<User> Register (string name, string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetPassword (string email)
    {
        throw new NotImplementedException();
    }

    public string SaltPassword (string password)
    {
        EncryptionConfig config = new EncryptionConfig();
        string salt = config.Salt();
        string saltedPassword = password + salt;
        string hashedPassword = config.Hash(saltedPassword);
        return hashedPassword;
    }

    public Task<bool> VerifyEmail (string email)
    {
        throw new NotImplementedException();
    }

}
