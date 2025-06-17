using BBTT.Web;
using BBTT.Web.Components;
using BBTT.Web.Hubs;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing.Constraints;

var builder = WebApplication.CreateBuilder(args);


// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();
builder.Services.AddProblemDetails();

#region HTTPClients
builder.Services.AddHttpClient<CrossWordApiClient>(client =>
{
    client.BaseAddress = new("https+http://crosswordapi");
    client.Timeout = TimeSpan.FromMinutes(5);
});
builder.Services.AddHttpClient<DataApiClient>(client =>
{
    client.BaseAddress = new("https+http://bbtt-dataapi");
    client.Timeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddHttpClient<AuthApiClient>(client =>
{
    client.BaseAddress = new("https+http://bbtt-authapi");
    client.Timeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddHttpClient<object>(client =>
{
    client.BaseAddress = new("https+http://user");
    client.Timeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddHttpClient<object>(client =>
{
    client.BaseAddress = new("https+http://employee");
    client.Timeout = TimeSpan.FromMinutes(5);
});

#endregion

#region SignalR
//builder.Services.AddResponseCompression(options =>
//{
//    options.EnableForHttps = true;
//    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
//        new [] { "application/octet-stream" });
//});

//builder.Services.AddSignalR(options =>
//{
//    options.EnableDetailedErrors = true;
//    options.MaximumReceiveMessageSize = 1024 * 1024 * 10; // 10 MB
//    options.MaximumParallelInvocationsPerClient = 10;    
//});

#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

//app.MapBlazorHub("/app");
app.MapHub<ChatHub>("/chathub");

app.Run();
