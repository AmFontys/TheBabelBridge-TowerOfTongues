using BBTT.CrosswordCore;
using BBTT.CrosswordModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BBTT.CrosswordAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CrosswordController : ControllerBase
{
    private readonly ILogger<CrosswordController> _logger;
    private readonly ICrosswordAccesor _crosswordAccesor;

    public CrosswordController(ILogger<CrosswordController> logger, ICrosswordAccesor crosswordAccesor)
    {
        _logger = logger;
        _crosswordAccesor = crosswordAccesor;
    }

    [HttpGet(Name = "GetCrosswordWords")]
    public IEnumerable<CrosswordWord> Get()
    {
        List<CrosswordWord> list =
        [
            new CrosswordWord("Apple", "Basic", "English", ""),
            new CrosswordWord("Orange", "Basic", "English", ""),
            new CrosswordWord("Banana", "Basic", "English", ""),
            new CrosswordWord("Pear", "Basic", "English", ""),
        ];
        return list;
    }

    [HttpPost(Name = "PostCrosswordGeneration")]
    public async Task<IActionResult> PostCrosswordGeneration(CrosswordWord[] words, CancellationToken cancellationToken)
    {
        if (words == null || words.Length == 0)
        {
            return BadRequest("Words cannot be null or empty.");
        }

        try
        {
            var result = await _crosswordAccesor.ConstructCrossword(words, cancellationToken);
            if (result == null || result.GridEntries == null || result.GridEntries.Count == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate crossword grid.");
            }

            result = _crosswordAccesor.AddBlankValuesToGrid(result);

            //var gridWithStringKeys = result.GridEntries.ToDictionary(
            //    kvp => kvp.Key.ToString(),
            //    kvp => kvp.Value
            //    );
            //string JsonString = JsonSerializer.Serialize(gridWithStringKeys);
            return Ok(result);
            //return Ok(JsonString);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status408RequestTimeout, "The operation was canceled due to timeout.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating crossword grid.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while generating the crossword grid.");
        }
    }
}
