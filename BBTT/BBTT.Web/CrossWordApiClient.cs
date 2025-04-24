using BBTT.CrosswordModel;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace BBTT.Web;

/// <summary>
/// The Api client for the Crossword functionality
/// </summary>
public class CrossWordApiClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="CrossWordApiClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client derived from the settings set in <see cref="Program"/> class.</param>
    public CrossWordApiClient (HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromMinutes(5); // Increase timeout to 5 minutes
    }
    /// <summary>
    /// Gets the dictionary.with words that can be used to fill in the crossword.
    /// </summary>
    /// <param name="maxItems">The maximum items.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<CrosswordWord []> GetDictionary (int maxItems = 100, CancellationToken cancellationToken = default)
    {
        List<CrosswordWord>? encyclopedia = null;

        await foreach (var forecast in _httpClient.GetFromJsonAsAsyncEnumerable<CrosswordWord>("/Crossword", cancellationToken))
        {
            if (encyclopedia?.Count >= maxItems)
            {
                break;
            }
            if (forecast is not null)
            {
                encyclopedia ??= [];
                encyclopedia.Add(forecast);
            }
        }

        return encyclopedia?.ToArray() ?? [];
    }

    /// <summary>
    /// Posts the words and gets the generated grid back.
    /// </summary>
    /// <param name="words">The words that will be used to generate the crossword.</param>
    /// <returns>The generated Crossword in the format of <see cref="CrosswordGrid"/></returns>
    public async Task<CrosswordGrid> PostWordsGetGrid (CrosswordWord [] words)
    {
        var response = await _httpClient.PostAsJsonAsync("/Crossword", words);
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful        
        CrosswordGrid? result = await response.Content.ReadFromJsonAsync<CrosswordGrid>();
        //TODO: Check on null failure
        return result;

    }
    /// <summary>
    /// Gets the closest word.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>A list of type <see cref="CrosswordWord"/></returns>
    public async Task<List<CrosswordWord>> GetClosestWord (string input)
    {
        List<CrosswordWord>? words = null;
        if (input == "App")
            words.Add(new CrosswordWord("Apple", "Basic", "English", ""));
        else
            words.Add(new CrosswordWord("Banana", "Basic", "English", ""));
        return words;
    }

    public async Task<List<CrosswordWord>> ReadCsvFile (IBrowserFile? input)
    {
        List<CrosswordWord>? words = null;

        if (input != null)
        {
            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(input.OpenReadStream(maxAllowedSize: 1024 * 1024 * 15)); // Adjust max size as needed
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(input.ContentType);
            content.Add(fileContent, "input", input.Name); // Ensure the name matches the API parameter

            var response = await _httpClient.PostAsync("/readcsv", content);
            if (response.IsSuccessStatusCode)
            {
                words = await response.Content.ReadFromJsonAsync<List<CrosswordWord>>();
            }
        }

        return words;
    }

}
