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

    public async Task<int> CreateCrossword(Crossword crossword)
    {
        CrosswordDto crosswordDto = new()
        {
            Name = crossword.Name,
            Description = crossword.Description,
            Tags = crossword.Tags,
            CrosswordGrid = new CrosswordGridDto()
            {
                GridEntries = new List<GridEntryDTO>(),
            },
            Words = crossword.crosswordWords.Select(word => new CrosswordWordDTO()
            {
                Word = word.Word,
                Direction = word.Direction,
                Diffuclty = word.Diffuclty,
                Language = word.Language,
                Hint = word.Hint,
            }).ToList()
        };

        // Add the crossword with its related words
        await _pgsqlDbContext.Crosswords.AddAsync(crosswordDto);
        await _pgsqlDbContext.SaveChangesAsync();

        return crosswordDto.Id;
    }

    public async Task<CrosswordDto> GetCrossword(int id)
    {
        var result = await _pgsqlDbContext.Crosswords
            .Include(c => c.CrosswordGrid)
            .ThenInclude(g => g.GridEntries)
            .FirstOrDefaultAsync(c => c.Id == id);
        return result;
    }

    public async Task<int> CreateCrosswordGrid (int id, CrosswordGrid crosswordGrid)
    {
        var crosswordGridDto = new CrosswordGridDto()
        {
            CrosswordId = id,
            GridEntries = new(),
        };
        foreach (var item in crosswordGrid.GridEntries)
        {
            crosswordGridDto.GridEntries.Add(new GridEntryDTO()
            {
                CrosswordGridId = crosswordGridDto.Id,
                Row = item.Row,
                Column = item.Column,
                Value = item.Value,
            });
        }
        try
        {
            await _pgsqlDbContext.CrosswordGrids.AddAsync(crosswordGridDto);
            await _pgsqlDbContext.SaveChangesAsync();
            return crosswordGridDto.Id;
        }
        catch (Exception)
        {
            return 0;
            throw;
        }
        
        
    }

    
    public async Task<CrosswordDto> GetCrossword (Crossword crosswordDto)
    {
        var result = await _pgsqlDbContext.Crosswords.FindAsync(crosswordDto);
        return result;
    }

    public Task<List<CrosswordDto>> GetCrosswords ()
    {
        var result = _pgsqlDbContext.Crosswords.ToListAsync();
        return result;
    }

    public Crossword MapCrosswords (CrosswordDto crossword)
    {
        Crossword _mappedCrossword = new()
        {            
            Name = crossword.Name,
            Description = crossword.Description,
            Tags = crossword.Tags != null ? crossword.Tags : new List<string>() { "None"},
            CrosswordGrid = crossword.CrosswordGrid != null ? new CrosswordGrid()
            {
                GridEntries = crossword.CrosswordGrid.GridEntries.Select(x => new GridEntry()
                {
                    Row = x.Row,
                    Column = x.Column,
                    Value = x.Value,
                }).ToList(),
            } : null,
        };
        return _mappedCrossword;
    }

    public List<Crossword> MapCrosswords (List<CrosswordDto> crosswords)
    {
        List<Crossword> _mappedCrosswords = new();
        foreach (var crossword in crosswords)
        {
            _mappedCrosswords.Add(MapCrosswords(crossword));
        }
        return _mappedCrosswords;
    }

    public Task<CrosswordDto> GetCrossword (string name)
    {
        var result = _pgsqlDbContext.Crosswords
            .Include(c => c.CrosswordGrid)
            .ThenInclude(g => g.GridEntries)
            .FirstOrDefaultAsync(c => c.Name == name);
        return result;
    }
}
