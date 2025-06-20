namespace BBTT.ApiService;

public static class CrosswordApiEndpoints
{
    public static void MapCrosswordApiEndpoints(this WebApplication app)
    {
        // Proxy: Get dictionary (streaming)
        app.MapGet("/Crossword", async (HttpContext context, IHttpClientFactory httpClientFactory, CancellationToken cancellationToken) =>
        {
            var client = httpClientFactory.CreateClient("CrosswordApi");
            using var response = await client.GetAsync("/Crossword", HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            context.Response.ContentType = "application/json";
            await response.Content.CopyToAsync(context.Response.Body, cancellationToken);
        });

        // Proxy: Post words and get generated grid
        app.MapPost("/Crossword", async (HttpRequest req, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("CrosswordApi");
            var content = new StreamContent(req.Body);
            foreach (var header in req.Headers)
                content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            var response = await client.PostAsync("/Crossword", content);
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        // Proxy: Read CSV file (file upload)
        app.MapPost("/readcsv", async (HttpRequest req, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("CrosswordApi");
            var content = new StreamContent(req.Body);
            foreach (var header in req.Headers)
                content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            var response = await client.PostAsync("/readcsv", content);
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        // Proxy: Get closest word (dummy implementation in client, but add for completeness)
        app.MapGet("/closestword/{input}", async (string input, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("CrosswordApi");
            var response = await client.GetAsync($"/closestword/{input}");
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });
    }
}
