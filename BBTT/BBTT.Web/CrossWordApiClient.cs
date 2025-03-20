using BBTT.CrosswordModel;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace BBTT.Web;

public class CrossWordApiClient
{
    private readonly HttpClient _httpClient;

    public CrossWordApiClient (HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromMinutes(5); // Increase timeout to 5 minutes
    }
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

    public async Task<CrosswordGrid> PostWordsGetGrid (CrosswordWord [] words)
    {
        var response = await _httpClient.PostAsJsonAsync("/Crossword", words);
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful        
        CrosswordGrid? result = await response.Content.ReadFromJsonAsync<CrosswordGrid>();
        //TODO: Check on null failure
        return result;

    }

    public async Task<List<CrosswordWord>> GetClosestWord (string input)
    {
        List<CrosswordWord>? words = null;
        if (input == "App")
            words.Add(new CrosswordWord("Apple", "Basic", "English", ""));
        else
            words.Add(new CrosswordWord("Banana", "Basic", "English", ""));
        return words;
    }


}
