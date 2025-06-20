using BBTT.CrosswordModel;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.RegularExpressions;
using UserModel;
namespace BBTT.Web;
/// <summary>
/// The data api client used to communicate with the authication api
/// </summary>
public class AuthApiClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthApiClient"/> class with the specified <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance used to send HTTP requests.</param>
    public AuthApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromMinutes(5);
    }

    public async Task testConnection()
    {
        var response = await _httpClient.GetAsync("/Auth/Authication");
        response.EnsureSuccessStatusCode();
    }

    public async Task<User> Login(string email, string password)
    {
        LoginModel model = new()
        {
            Email = email,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync("/Auth/CheckLogin", model);
        response.EnsureSuccessStatusCode();
        User? result = await response.Content.ReadFromJsonAsync<User>();
        return result;
    }

    public async Task<bool> Register(string name, string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/Auth/register", new { name, email, password });
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> Logout()
    {
        var response = await _httpClient.PostAsync("/Auth/logout", null);
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> ChangePassword(string email, string oldPassword, string newPassword)
    {
        var response = await _httpClient.PostAsJsonAsync("/Auth/changePassword", new { email, oldPassword, newPassword });
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> ResetPassword(string email)
    {
        var response = await _httpClient.PostAsJsonAsync("/Auth/resetPassword", new { email });
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> VerifyEmail(string email)
    {
        var response = await _httpClient.PostAsJsonAsync("/Auth/verifyEmail", new { email });
        response.EnsureSuccessStatusCode();
        return true;
    }
}
