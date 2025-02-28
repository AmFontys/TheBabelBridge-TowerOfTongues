using BBTT.CrosswordCore;
using BBTT.CrosswordModel;
using Microsoft.AspNetCore.Mvc;

namespace BBTT.CrosswordAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CrosswordController : ControllerBase
{    
    private readonly ILogger<CrosswordController> _logger;
    private readonly ICrosswordAccesor _crosswordAccesor;

    public CrosswordController (ILogger<CrosswordController> logger, ICrosswordAccesor crosswordAccesor)
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

    [HttpPost(Name ="PostCrosswordGeneration")]
    public async Task<string> PostCrosswordGeneration (CrosswordWord[] words, CancellationToken cancellationToken)
    {
        var result= await _crosswordAccesor.ConstructCrossword(words,cancellationToken);
        //TODO: add something to handle the returned grid
        return "Done";
    }
}
