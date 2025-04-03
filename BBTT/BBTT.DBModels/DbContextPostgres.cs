using BBTT.DBModels.Crossword;
using Microsoft.EntityFrameworkCore;

namespace BBTT.DBModels;

public class DbContextPostgres : DbContext
{
    public DbContextPostgres(DbContextOptions options) : base(options) { }

    public DbSet<CrosswordDto> Crosswords => Set<CrosswordDto>();
    public DbSet<CrosswordGridDto> CrosswordGrids => Set<CrosswordGridDto>();
    public DbSet<GridEntryDTO> CrosswordGridEntries => Set<GridEntryDTO>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrosswordDto>()
            .HasOne(c => c.CrosswordGrid)
            .WithOne(g => g.Crossword)
            .HasForeignKey<CrosswordGridDto>(g => g.CrosswordId);

        modelBuilder.Entity<CrosswordGridDto>()
            .HasMany(g => g.GridEntries)
            .WithOne(e => e.CrosswordGrid)
            .HasForeignKey(e => e.CrosswordGridId);

        base.OnModelCreating(modelBuilder);
    }
}
