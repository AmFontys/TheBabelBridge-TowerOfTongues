
using BBTT.AuthCore;

namespace BBTT.AuthApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Register AuthAccesor with HttpClient configured for the API gateway
        builder.Services.AddHttpClient<IAuthAccesor, AuthAccesor>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7518");
            client.Timeout = TimeSpan.FromMinutes(5);
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapDefaultEndpoints();

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
