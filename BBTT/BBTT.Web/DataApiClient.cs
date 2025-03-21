using BBTT.CrosswordModel;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.RegularExpressions;
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
    public async Task<CrosswordGrid> GetGrid (int id)
    {
        var response = await _httpClient.GetAsync($"/Crossword/{id}");
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful        
        CrosswordGrid? result = await response.Content.ReadFromJsonAsync<CrosswordGrid>();
        return result;
    }
    /// <summary>
    /// Saves the crossword.
    /// </summary>
    /// <param name="crossword">The crossword.</param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> SaveCrossword(Crossword crossword)
    {
        var response = await _httpClient.PostAsJsonAsync("/Databaset", crossword);
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful        
        return response;
    }
}
