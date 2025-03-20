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
}
