using BBTT.Web;
using BBTT.Web.Components;
using Microsoft.AspNetCore.Routing.Constraints;

var builder = WebApplication.CreateBuilder(args);


// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();

builder.Services.AddHttpClient<CrossWordApiClient>(client =>
{
    client.BaseAddress = new("https+http://crosswordapi");    
});
builder.Services.AddHttpClient<DataApiClient>(client =>
{
    client.BaseAddress = new("https+http://bbtt-dataapi");
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

app.Run();
