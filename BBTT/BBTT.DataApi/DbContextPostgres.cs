using BBTT.DBModels.Crossword;
using Microsoft.EntityFrameworkCore;

namespace BBTT.DataApi;

public class DbContextPostgres : DbContext
{
    public DbContextPostgres(DbContextOptions options) : base(options) { }

    public DbSet<CrosswordDto> Crossword => Set<CrosswordDto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrosswordGridDto>()
            .HasMany(c => c.GridEntries)
            .WithOne()
            .HasForeignKey(e => e.CrosswordGridId);

        base.OnModelCreating(modelBuilder);
    }
}
