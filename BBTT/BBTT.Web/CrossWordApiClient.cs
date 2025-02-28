using BBTT.CrosswordModel;
using System.Net.Http;
namespace BBTT.Web;

public class CrossWordApiClient(HttpClient httpClient)
{
    public async Task<CrosswordWord []> GetDictionary (int maxItems = 100, CancellationToken cancellationToken = default)
    {
        List<CrosswordWord>? encyclopedia = null;

        await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<CrosswordWord>("/Crossword", cancellationToken))
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

    public async Task<string> PostWordsGetGrid (CrosswordWord[] words, CancellationToken cancellationToken = default)
    {
        foreach (var word in words)
        {
            if (string.IsNullOrEmpty(word.Word) || string.IsNullOrEmpty(word.Direction))
            {
                throw new ArgumentException("Word and Direction must be provided.");
            }
        }
        var response = await httpClient.PostAsJsonAsync("/Crossword", words, cancellationToken);
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }

    public async Task<List<CrosswordWord>> GetClosestWord(string input)
    {
        List<CrosswordWord>? words = null;
        if (input == "App")
            words.Add(new CrosswordWord("Apple", "Basic", "English",""));
        else
            words.Add(new CrosswordWord("Banana","Basic","English",""));
        return words;
    }

}