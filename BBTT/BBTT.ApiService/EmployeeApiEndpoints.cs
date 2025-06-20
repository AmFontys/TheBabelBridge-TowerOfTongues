namespace BBTT.ApiService;

public static class EmployeeApiEndpoints
{
    public static void MapEmployeeApiEndpoints(this WebApplication app)
    {
        // Proxy: Post to /Employee
        app.MapPost("/Employee", async (HttpRequest req, IHttpClientFactory httpClientFactory) =>
        {
            var client = httpClientFactory.CreateClient("EmployeeApi");
            var content = new StreamContent(req.Body);
            foreach (var header in req.Headers)
                content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            var response = await client.PostAsync("/Employee", content);
            response.EnsureSuccessStatusCode();
            return Results.Content(await response.Content.ReadAsStringAsync(), "application/json");
        });
    }
}
