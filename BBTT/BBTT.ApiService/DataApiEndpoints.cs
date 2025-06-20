namespace BBTT.ApiService;

public static class DataApiEndpoints
{
    public static void MapDataApiEndpoints(this WebApplication app)
    {
        app.MapGet("/Crossword/{id:int}", async (int id, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("DataApi");
            var response = await client.GetAsync($"/Crossword/{id}");
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        app.MapGet("/Databaset/{name}/get", async (string name, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("DataApi");
            var response = await client.GetAsync($"/Databaset/{name}/get");
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        app.MapGet("/Databaset", async (IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("DataApi");
            var response = await client.GetAsync("/Databaset");
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

        app.MapPost("/Databaset", async (HttpRequest req, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("DataApi");
            var content = new StreamContent(req.Body);
            foreach (var header in req.Headers)
                content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            var response = await client.PostAsync("/Databaset", content);
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });

    }
}
