using BBTT.DataApi.Controllers;
using BBTT.DBModels.Crossword;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BBTT.DataApi;

public class Service
{
    private readonly DbContextPostgres _dataSource;

    public Service(DbContextPostgres dataSource)
    {
        _dataSource = dataSource;
    }

    public static async Task<IEnumerable<CrosswordDto>> GetCrossword()
    {
        var _dataSource = new DbContextPostgres(new DbContextOptions<DbContextPostgres>());
        var crosswords = await _dataSource.Crossword.ToListAsync();
        if (crosswords == null)
        {
            return null;
        }
        else
        {
            return crosswords;
        }
    }
}
