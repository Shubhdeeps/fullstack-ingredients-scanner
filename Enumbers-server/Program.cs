using Enumbers_server.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
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
app.MapOCREndpoints();
app.MapGet("/", () => Results.Text("Hello, this is ocr engine api!"));
app.UseCors(MyAllowSpecificOrigins);
app.Run();
