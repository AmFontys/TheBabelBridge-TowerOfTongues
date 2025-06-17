using BBTT.DBModels.Crossword;
using BBTT.DBModels.Email;
using BBTT.DBModels.User;
using Microsoft.EntityFrameworkCore;

namespace BBTT.DBModels;

public class DbContextPostgres : DbContext
{
    public DbContextPostgres(DbContextOptions options) : base(options) { }

    //Crossword
    public DbSet<CrosswordDTO> Crosswords => Set<CrosswordDTO>();
    public DbSet<CrosswordGridDto> CrosswordGrids => Set<CrosswordGridDto>();
    public DbSet<GridEntryDTO> CrosswordGridEntries => Set<GridEntryDTO>();

    public DbSet<CrosswordWordDTO> CrosswordWords => Set<CrosswordWordDTO>();
    // User
    public DbSet<UserDTO> Users => Set<UserDTO>();
    public DbSet<UserCrosswordDTO> UserCrosswords => Set<UserCrosswordDTO>();
    public DbSet<UserRolesDTO> UserRoles => Set<UserRolesDTO>();

    // Email
    public DbSet<EmailVerficationDTO> Emails => Set<EmailVerficationDTO>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrosswordDTO>()
            .HasOne(c => c.CrosswordGrid)
            .WithOne(g => g.Crossword)
            .HasForeignKey<CrosswordGridDto>(g => g.CrosswordId);

        modelBuilder.Entity<CrosswordGridDto>()
            .HasMany(g => g.GridEntries)
            .WithOne(e => e.CrosswordGrid)
            .HasForeignKey(e => e.CrosswordGridId);

        modelBuilder.Entity<CrosswordDTO>()
        .HasMany(c => c.Words)
        .WithOne()
        .HasForeignKey(w => w.CrosswordId)
        .OnDelete(DeleteBehavior.Cascade); // Optional: Configure cascade delete

        //User
        modelBuilder.Entity<UserDTO>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(r => r.RoleId);


        modelBuilder.Entity<UserCrosswordDTO>()
            .HasKey(uc => new { uc.UserId, uc.CrosswordId }); // Composite key

        modelBuilder.Entity<UserCrosswordDTO>()
        .HasOne(uc => uc.User)
        .WithMany(u => u.UserCrosswords)
        .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserCrosswordDTO>()
            .HasOne(uc => uc.Crossword)
            .WithMany()
            .HasForeignKey(uc => uc.CrosswordId);

        modelBuilder.Entity<EmailVerficationDTO>()
            .HasKey(e => e.Id);

        base.OnModelCreating(modelBuilder);
    }
}
