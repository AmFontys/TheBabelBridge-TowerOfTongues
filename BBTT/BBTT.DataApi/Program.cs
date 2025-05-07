
using BBTT.DataApi.Controllers;
using BBTT.DBModels;
using BBTT.DBPostgres;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Text.Json.Serialization;

namespace BBTT.DataApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.AddNpgsqlDbContext<DbContextPostgres>("bbttdb");
        builder.Services.AddDbContext<DbContextPostgres>();
        builder.Services.AddScoped<ICrosswordDataAccess, CrosswordDataAcess>();
        builder.Services.AddScoped<IUserDataAcess, UserDataAcess>();

        builder.Services.AddControllers()
             .AddJsonOptions(options =>
             {
                 //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
             });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapDefaultEndpoints();
        app.MapPgsqlAspireEndpoint();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
