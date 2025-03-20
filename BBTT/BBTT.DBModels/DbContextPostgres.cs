using BBTT.DBModels.Crossword;
using Microsoft.EntityFrameworkCore;

namespace BBTT.DBModels;

public class DbContextPostgres : DbContext
{
    public DbContextPostgres (DbContextOptions options) : base(options) { }

    public DbSet<CrosswordDto> Crossword => Set<CrosswordDto>();
    public DbSet<CrosswordGridDto> CrosswordGrid => Set<CrosswordGridDto>();
    public DbSet<GridEntryDTO> CrosswordGridEntry => Set<GridEntryDTO>();

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrosswordGridDto>()
            .HasMany(c => c.GridEntries)
            .WithOne()
            .HasForeignKey(e => e.CrosswordGridId);

        modelBuilder.Entity<GridEntryDTO>()
            .HasKey(e => new { e.Row, e.Column, e.CrosswordGridId });

        base.OnModelCreating(modelBuilder);
    }
}
