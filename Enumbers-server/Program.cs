using Enumbers_server.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndpoints();
app.MapOCREndpoints();
app.MapGet("/", () => Results.Text("Hello, world!"));
app.Run();
