var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.BBTT_ApiService>("apiservice");

builder.AddProject<Projects.BBTT_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
