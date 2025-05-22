
using BBTT.AuthCore;

namespace BBTT.AuthApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
                
        //builder.Services.AddHttpClient<IAuthAccesor, AuthAccesor>(client =>
        //{
        //    client.BaseAddress = new Uri("https+http://bbtt-authapi");
        //    client.Timeout = TimeSpan.FromMinutes(5);
        //});

        builder.Services.AddSingleton<IAuthAccesor, AuthAccesor>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapDefaultEndpoints();

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
