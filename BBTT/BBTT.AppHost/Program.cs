using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

#region SignalR
//var signalR = builder.AddAzureSignalR("signalr", AzureSignalRServiceMode.Serverless)
//                     .RunAsEmulator();
#endregion
#region Postgres
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin(pgAdmin  => pgAdmin.WithHostPort(5050))
    .WithDataVolume(isReadOnly: false);
var postgresdb = postgres.AddDatabase("bbttdb");
var dataApi =builder.AddProject<Projects.BBTT_DataApi>("bbtt-dataapi")
    .WithReference(postgresdb);
#endregion

#region api's
var crosswordAPI = builder.AddProject<Projects.BBTT_CrosswordAPI>("crosswordapi")
    //.WithReference(signalR)
    //.WaitFor(signalR)
    ;

var authapi = builder.AddProject<Projects.BBTT_AuthApi>("bbtt-authapi")
    //.WithReference(dataApi)
    //.WaitFor(dataApi)
    ;

var apiService = builder.AddProject<Projects.BBTT_ApiService>("apiservice")
    .WithReference(crosswordAPI)
    .WithReference(authapi)
    .WithReference(dataApi)
   ;
#endregion



builder.AddProject<Projects.BBTT_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    //Refrences
    .WithReference(apiService)
    .WaitFor(apiService)
    ;





await builder.Build().RunAsync();
