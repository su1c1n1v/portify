var builder = DistributedApplication.CreateBuilder(args);

var api = builder
    .AddProject<Projects.PortifyAPI>("api")
    .WithExternalHttpEndpoints();

builder
    .AddJavaScriptApp("web", "../../portify-web", "start")
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();
