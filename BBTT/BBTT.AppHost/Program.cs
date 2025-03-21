var builder = DistributedApplication.CreateBuilder(args);


var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin(pgAdmin  => pgAdmin.WithHostPort(5050))
    .WithDataVolume(isReadOnly: false);
var postgresdb = postgres.AddDatabase("bbttdb");
var dataApi =builder.AddProject<Projects.BBTT_DataApi>("bbtt-dataapi")
    .WithReference(postgresdb);

var apiService = builder.AddProject<Projects.BBTT_ApiService>("apiservice");
var crosswordAPI = builder.AddProject<Projects.BBTT_CrosswordAPI>("crosswordapi");

builder.AddProject<Projects.BBTT_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    //Refrences
    .WithReference(apiService)
    .WithReference(crosswordAPI)
    .WithReference(dataApi)
    //Waiting statements
    .WaitFor(crosswordAPI)
    .WaitFor(apiService)
    .WaitFor(dataApi);


await builder.Build().RunAsync();
