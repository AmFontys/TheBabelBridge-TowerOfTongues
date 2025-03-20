using BBTT.CrosswordModel;
using BBTT.DBModels;
using BBTT.DBModels.Crossword;
using Microsoft.EntityFrameworkCore;

namespace BBTT.DBPostgres;

public class CrosswordDataAcess : ICrosswordDataAccess
{
    DbContextPostgres _pgsqlDbContext;

    public CrosswordDataAcess (DbContextPostgres pgsqlDbContext)
    {
        _pgsqlDbContext = pgsqlDbContext;
    }

    public async Task CreateCrossword (Crossword crossword)
    {
        CrosswordDto crosswordDto = new()
        {
            Name = crossword.Name,
            Description = crossword.Description,
            Tags = crossword.Tags,
        };
        await _pgsqlDbContext.Crossword.AddAsync(crosswordDto);        
        await _pgsqlDbContext.SaveChangesAsync();                
    }

    public async Task<CrosswordDto> GetCrossword (int id)
    {
        return await _pgsqlDbContext.Crossword.FindAsync(id);
    }

    public async Task CreateCrosswordGrid (Task<CrosswordDto> result, CrosswordGrid crosswordGrid)
    {
        var crosswordGridDto = new CrosswordGridDto()
        {
            CrosswordId = result.Id,
            GridEntries = new(),
        };
        foreach (var item in crosswordGrid.GridEntries)
        {
            crosswordGridDto.GridEntries.Add(new GridEntryDTO()
            {
                Row = item.Row,
                Column = item.Column,
                Value = item.Value,
            });
        }
        await _pgsqlDbContext.CrosswordGrid.AddAsync(crosswordGridDto);
        await _pgsqlDbContext.SaveChangesAsync();
    }

    
    public async Task<CrosswordDto> GetCrossword (Crossword crosswordDto)
    {
        var result = await _pgsqlDbContext.Crossword.FindAsync(crosswordDto);
        return result;
    }
}
