using BBTT.CrosswordModel;
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
    public async Task<CrosswordWord[]> GetDictionary(int maxItems = 100, CancellationToken cancellationToken = default)
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
        var resultString = await response.Content.ReadAsStringAsync();
        return DeserializeCrosswordGrid(resultString);
    }

    public async Task<List<CrosswordWord>> GetClosestWord(string input)
    {
        List<CrosswordWord>? words = null;
        if (input == "App")
            words.Add(new CrosswordWord("Apple", "Basic", "English", ""));
        else
            words.Add(new CrosswordWord("Banana", "Basic", "English", ""));
        return words;
    }

    private static CrosswordGrid DeserializeCrosswordGrid(string serializedGrid)
    {
        CrosswordGrid? grid = new();

        try
        {

            string pattern = @"\(\s*(-?\d+)\s*,\s*(-?\d+)\s*\)"":""\s*([\w\@])";
            

            foreach (Match match in Regex.Matches(serializedGrid, pattern))
            {
                int firstDigit=(int.Parse(match.Groups [ 1 ].Value));
                int secondDigit=(int.Parse(match.Groups [ 2 ].Value));
                char value =(char.Parse(match.Groups [ 3 ].Value));
                grid.Grid.Add((firstDigit, secondDigit), value);
            }


            
            if (grid == null)
            {
                throw new InvalidOperationException("Deserialization returned null.");
            }
            return grid;
        }
        catch (Exception ex)
        {
            // Log the exception (use your preferred logging mechanism)
            Console.WriteLine($"Deserialization error: {ex.Message}");
            throw;
        }
    }

}
