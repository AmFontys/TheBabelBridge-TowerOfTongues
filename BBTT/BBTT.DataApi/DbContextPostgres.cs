using BBTT.DBModels.Crossword;
using Microsoft.EntityFrameworkCore;

namespace BBTT.DataApi;

public class DbContextPostgres : DbContext
{
    public DbContextPostgres(DbContextOptions<DbContextPostgres> options) : base(options)
    {
    }

    // Define DbSets for your entities here
    // public DbSet<YourEntity> YourEntities { get; set; }
    public DbSet<CrosswordDto> Crossword { get; set; }
    public DbSet<CrosswordGridDto> CrosswordGrid { get; set; }
}
