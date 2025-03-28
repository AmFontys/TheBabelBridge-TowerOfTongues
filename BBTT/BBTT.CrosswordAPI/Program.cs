using BBTT.CrosswordCore;
using BBTT.Files;

namespace BBTT.CrosswordAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddProblemDetails();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

        app.Run();
    }
}
