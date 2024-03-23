using Enumbers_server.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//set app host and port
app.Urls.Add("http://localhost:9090");

app.MapGamesEndpoints();
app.MapOCREndpoints();
app.MapGet("/", () => Results.Text("Hello, world!"));
app.Run();
