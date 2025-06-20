using BBTT.ApiService;
using Microsoft.Extensions.Configuration; // Ensure this namespace is included for extension methods
using Microsoft.Extensions.DependencyInjection; // Add this namespace for service-related extensions
using Microsoft.Extensions.Http; // Add this namespace for HttpClient-related extensions
using Microsoft.AspNetCore.Http.Extensions; // Add this namespace for Uri-related extensions

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Use Aspire's service discovery for internal service URIs
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();

var app = builder.Build();

app.UseExceptionHandler();
app.MapDefaultEndpoints();

// Additional configuration for the app
app.UseForwardedHeaders();
app.UseResponseCaching();


app.MapReverseProxy();
//// Map the Data API endpoints
//app.MapDataApiEndpoints();
//// Map the Auth API endpoints
//app.MapAuthApiEndpoints();
//// Map the Crossword API endpoints
//app.MapCrosswordApiEndpoints();
//// Map the Employee API endpoints
//app.MapEmployeeApiEndpoints();

await app.RunAsync();
