var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.BBTT_ApiService>("apiservice");
var crosswordAPI = builder.AddProject<Projects.BBTT_CrosswordAPI>("crosswordapi");

builder.AddProject<Projects.BBTT_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    //Refrences
    .WithReference(apiService)
    .WithReference(crosswordAPI)
    //Waiting statements
    .WaitFor(crosswordAPI)
    .WaitFor(apiService);

builder.Build().Run();
