using BBTT.DBModels;
using BBTT.DBModels.Crossword;
using Microsoft.EntityFrameworkCore;

namespace BBTT.DataApi;

public static class DBContextPostgresExtension
{
    public static void MapPgsqlAspireEndpoint (this WebApplication app)
    {
        app.MapGet("/testConnection", async (DbContextPostgres pgsqlDbContext) =>
        {
            await pgsqlDbContext.Crosswords.AddAsync(new CrosswordDTO()
            {
                Name = "Test",
                Description = "Test",
                Tags = new List<string>() { "Test" },
                CrosswordGrid = new CrosswordGridDto()
                {
                    GridEntries = new List<GridEntryDTO>(),
                },

            });
            int rows = await pgsqlDbContext.SaveChangesAsync();
            if (rows > 0)
            {
                return await pgsqlDbContext.Crosswords.FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }
        });

        //if (app.Environment.IsDevelopment())
        //{
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DbContextPostgres>();
                context.Database.EnsureCreated();
            }
        //}
    }
}
