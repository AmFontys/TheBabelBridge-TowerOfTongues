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
    /// Initializes a new instance of the <see cref="AuthApiClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public AuthApiClient (HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromMinutes(5); // Increase timeout to 5 minutes
    }

    public async Task testConnection()
    {
        var response = await _httpClient.GetAsync("/Authication");
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
    }
    public async Task<User> Login (string email, string password)
    {
        LoginModel model = new()
        {
            Email = email,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync("/CheckLogin", model);
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
        User? result = await response.Content.ReadFromJsonAsync<User>();
        return result;
    }

    public async Task<bool> Register (string name, string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/Auth/register", new { name, email, password });
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
        return true;
    }

    public async Task<bool> Logout ()
    {
        var response = await _httpClient.PostAsync("/Auth/logout", null);
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
        return true;
    }

    public async Task<bool> ChangePassword (string email, string oldPassword, string newPassword)
    {
        var response = await _httpClient.PostAsJsonAsync("/Auth/changePassword", new { email, oldPassword, newPassword });
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
        return true;
    }

    public async Task<bool> ResetPassword (string email)
    {
        var response = await _httpClient.PostAsJsonAsync("/Auth/resetPassword", new { email });
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
        return true;
    }

    public async Task<bool> VerifyEmail (string email)
    {
        var response = await _httpClient.PostAsJsonAsync("/Auth/verifyEmail", new { email });
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
        return true;
    }
}
