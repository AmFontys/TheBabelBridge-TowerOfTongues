using BBTT.CrosswordCore;
using BBTT.CrosswordModel;
using BBTT.Files;
using Microsoft.AspNetCore.Mvc;

namespace BBTT.CrosswordAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CrosswordController (ILogger<CrosswordController> logger, ICrosswordAccesor crosswordAccesor, ICsvReaderAcessor csvReaderAcessor) : ControllerBase
{
    private const string Diffuclty = "Basic";
    private readonly ILogger<CrosswordController> _logger = logger;
    private readonly ICrosswordAccesor _crosswordAccesor = crosswordAccesor;
    private readonly ICsvReaderAcessor _csvReaderAcessor = csvReaderAcessor;

    [HttpGet(Name = "GetCrosswordWords")]
    public IEnumerable<CrosswordWord> Get ()
    {
        List<CrosswordWord> list =
        [
            new CrosswordWord("Apple", Diffuclty, "English", ""),
            new CrosswordWord("Orange", Diffuclty, "English", ""),
            new CrosswordWord("Banana", Diffuclty, "English", ""),
            new CrosswordWord("Pear", Diffuclty, "English", ""),
        ];
        return list;
    }

    [HttpPost(Name = "PostCrosswordGeneration")]
    public async Task<IActionResult> PostCrosswordGeneration (CrosswordWord [] words, CancellationToken cancellationToken)
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

            return Ok(result);
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

    [HttpGet("{id}", Name = "GetCrosswordGrid")]
    public async Task<IActionResult> GetCrosswordGrid (int id)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting the crossword grid.");
    }

    [HttpPost("/readcsv", Name = "PostCsv")]
    public async Task<IActionResult> PostCsv (IFormFile? input)
    {
        if (input == null || input.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            using var stream = input.OpenReadStream();
            var result = await _csvReaderAcessor.ReadWordsFromCsv(stream);
            if (result == null || !result.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read the CSV file.");
            }
            return Ok(result);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status408RequestTimeout, "The operation was canceled due to timeout.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while reading the CSV file: {ex.Message}");
        }
    }
}
