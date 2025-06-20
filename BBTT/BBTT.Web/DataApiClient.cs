using BBTT.CrosswordModel;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.RegularExpressions;
using UserModel;
namespace BBTT.Web;
/// <summary>
/// The data api client used to communicate with the data api
/// </summary>
public class DataApiClient
{
    private readonly HttpClient _httpClient;
    /// <summary>
    /// Initializes a new instance of the <see cref="DataApiClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public DataApiClient (HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromMinutes(5); // Increase timeout to 5 minutes
    }
    /// <summary>
    /// Gets the grid.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public async Task<CrosswordGrid> GetGrid(int id)
    {
        var response = await _httpClient.GetAsync($"/data/Crossword/{id}");
        response.EnsureSuccessStatusCode();
        CrosswordGrid? result = await response.Content.ReadFromJsonAsync<CrosswordGrid>();
        return result;
    }

    public async Task<Crossword> GetCrosswordAsync(string name)
    {
        var response = await _httpClient.GetAsync($"/data/Databaset/{name}/get");
        response.EnsureSuccessStatusCode();
        try
        {
            Crossword? result = await response.Content.ReadFromJsonAsync<Crossword>();
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        return null;
    }

    /// <summary>
    /// Gets the crosswords asynchronous.
    /// </summary>
    /// <returns>list of <see cref="Crossword"/> </returns>
    public async Task<List<Crossword>> GetCrosswordsAsync()
    {
        var response = await _httpClient.GetAsync("/data/Databaset");
        response.EnsureSuccessStatusCode();
        try
        {
            List<Crossword>? result = await response.Content.ReadFromJsonAsync<List<Crossword>>();
            if (result != null && result.Count > 0)
            {
                return result;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return null;
    }

    /// <summary>
    /// Saves the crossword.
    /// </summary>
    /// <param name="crossword">The crossword.</param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> SaveCrossword(Crossword crossword)
    {
        var response = await _httpClient.PostAsJsonAsync("/data/Databaset", crossword);
        response.EnsureSuccessStatusCode();
        return response;
    }

    public async Task<User> Login(string email, string password)
    {
        var user = new LoginModel
        {
            Email = email,
            Password = password
        };
        var response = await _httpClient.PostAsJsonAsync("/data/login", user);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<User>();
        return result;
    }

    public async Task<User> Register(string name, string email, string password)
    {
        var user = new RegisterModel
        {
            Name = name,
            Email = email,
            Password = password
        };
        var response = await _httpClient.PostAsJsonAsync("/data/Register", user);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<User>();
        return result;
    }

    public async Task<bool> SendEmailToUserForAuth(string email)
    {
        var response = await _httpClient.PostAsJsonAsync($"/data/Email/{email}", email);
        response.EnsureSuccessStatusCode();
        return true;
    }
    public async Task<bool> VerifyEmailCode(string email, string code)
    {
        VerficationModel model = new VerficationModel
        {
            Email = email,
            Code = code
        };

        var response = await _httpClient.PostAsJsonAsync("/data/Email/verify", model);
        response.EnsureSuccessStatusCode();
        return true;
    }

}
