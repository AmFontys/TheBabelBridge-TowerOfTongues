namespace BBTT.ApiService;

public static class AuthApiEndpoints
{
    public static void MapAuthApiEndpoints(this WebApplication app)
    {
        // Proxy: Test connection
        app.MapGet("/Authication", async (IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("AuthApi");
            var response = await client.GetAsync("/Authication");
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        // Proxy: Login
        app.MapPost("/CheckLogin", async (HttpRequest req, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("AuthApi");
            var content = new StreamContent(req.Body);
            foreach (var header in req.Headers)
                content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            var response = await client.PostAsync("/CheckLogin", content);
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        // Proxy: Register
        app.MapPost("/Auth/register", async (HttpRequest req, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("AuthApi");
            var content = new StreamContent(req.Body);
            foreach (var header in req.Headers)
                content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            var response = await client.PostAsync("/Auth/register", content);
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        // Proxy: Logout
        app.MapPost("/Auth/logout", async (HttpRequest req, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("AuthApi");
            var content = new StreamContent(req.Body);
            foreach (var header in req.Headers)
                content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            var response = await client.PostAsync("/Auth/logout", content);
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        // Proxy: Change password
        app.MapPost("/Auth/changePassword", async (HttpRequest req, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("AuthApi");
            var content = new StreamContent(req.Body);
            foreach (var header in req.Headers)
                content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            var response = await client.PostAsync("/Auth/changePassword", content);
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        // Proxy: Reset password
        app.MapPost("/Auth/resetPassword", async (HttpRequest req, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("AuthApi");
            var content = new StreamContent(req.Body);
            foreach (var header in req.Headers)
                content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            var response = await client.PostAsync("/Auth/resetPassword", content);
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        // Proxy: Verify email
        app.MapPost("/Auth/verifyEmail", async (HttpRequest req, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("AuthApi");
            var content = new StreamContent(req.Body);
            foreach (var header in req.Headers)
                content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            var response = await client.PostAsync("/Auth/verifyEmail", content);
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });
    }
}
