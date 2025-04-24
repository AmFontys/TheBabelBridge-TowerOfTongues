using BBTT.CrosswordCore;
using BBTT.Files;
using Microsoft.Azure.SignalR.Management;
using System.Text.Json.Serialization;
using System.Text.Json;
using BBTT.CrosswordAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BBTT.CrosswordAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add SignalR services
        builder.Services.AddSignalR();

        //SignalR service manager
        //builder.Services.AddSingleton(sp =>
        //{
        //    return new ServiceManagerBuilder()
        //        .WithOptions(options =>
        //        {
        //            options.ConnectionString = builder.Configuration.GetConnectionString("signalr");
        //        })
        //        .BuildServiceManager();
        //});

        // Add services to the container.
        builder.Services.AddProblemDetails();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Depency Injection (DI)
        builder.Services.AddSingleton<ICrosswordGenerator, CrosswordGenerator>();
        builder.Services.AddSingleton<ICrosswordAccesor, CrosswordAccesor>();
        builder.Services.AddSingleton<ICsvReaderAcessor, CsvReaderAcessor>();

        var app = builder.Build();
        app.UseExceptionHandler();

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

        //SignalR endpoints
        //app.MapPost("/negotiate", async (string? userId, ServiceManager sm, CancellationToken token) =>
        //{
        //    var context = await sm.CreateHubContextAsync("signalrhub", token);
        //    var negotiateResponse = await context.NegotiateAsync(new NegotiationOptions
        //    {
        //        UserId = userId
        //    }, token);

        //    return Results.Json(negotiateResponse, new JsonSerializerOptions(JsonSerializerDefaults.Web)
        //    {
        //        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        //    });
        //});

        app.MapHub<SignalRHub>("/signalrhub");

        app.Run();
    }
}
