using BBTT.CrosswordModel;
using BBTT.DBModels;
using BBTT.DBModels.Crossword;
using BBTT.DBPostgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBTT.DataApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabasetController : ControllerBase
{     
    private readonly ICrosswordDataAccess _crosswordDataAcess;

    public DatabasetController (ICrosswordDataAccess crosswordDataAccess)
    {
        _crosswordDataAcess = crosswordDataAccess ?? throw new ArgumentNullException(nameof(crosswordDataAccess));
    }

    [ HttpPost]
    public async Task<IActionResult> Get(DbContextPostgres pgsqlDbContext, Crossword crossword)
    {
        var id= await _crosswordDataAcess.CreateCrossword(crossword);        
        var result =await _crosswordDataAcess.CreateCrosswordGrid(id, crossword.CrosswordGrid);
        if(result == 0)
                return NotFound();

        return Ok();
       
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCrosswords ()
    {
        var crosswords = await _crosswordDataAcess.GetCrosswords();
        List<Crossword> returnList = _crosswordDataAcess.MapCrosswords(crosswords);
        if (crosswords == null)
            return NotFound();
        return Ok(returnList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCrossword (int id)
    {
        var crossword = await _crosswordDataAcess.GetCrossword(id);        
        var mapCrossword = _crosswordDataAcess.MapCrosswords(crossword);
        if (crossword == null)
            return NotFound();
        return Ok(mapCrossword);
    }

    [HttpGet("{name}/get")]
    public async Task<IActionResult> GetCrosswordByName (string name)
    {
        var crossword = await _crosswordDataAcess.GetCrossword(name);
        var mapCrossword = _crosswordDataAcess.MapCrosswords(crossword);
        if (crossword == null)
            return NotFound();
        return Ok(mapCrossword);
    }

}
