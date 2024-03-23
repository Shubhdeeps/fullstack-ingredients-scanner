using Enumbers_server.Api.Endpoints;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173/recognize",
                                              "http://localhost:5173/").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                      });
});


var app = builder.Build();
app.MapGamesEndpoints();
app.MapOCREndpoints();
app.MapGet("/", () => Results.Text("Hello, world!"));
app.UseCors(MyAllowSpecificOrigins);
app.Run();
