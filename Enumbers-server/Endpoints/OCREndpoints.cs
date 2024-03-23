using Enumbers_server.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Tesseract;

namespace Enumbers_server.Api.Endpoints;

public static class OCREndpoints
{
    private static string currentDirectory = Directory.GetCurrentDirectory();
    private static string fullPath = Path.Combine(currentDirectory, "bin", "Debug", "tessdata");
    public static async Task<byte[]> GetImageBytes(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return null; // Handle empty file scenario
        }

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
    public static WebApplication MapOCREndpoints(this WebApplication app)
    {

        app.MapPost("/recognize", async ([FromForm] OCRDto octDto) =>
        {
            var imageFile = octDto.ImageFile;
            Console.WriteLine($"Current Directory is: {octDto.ImageText}");
            var ocrengine = new TesseractEngine(@fullPath, "eng", EngineMode.Default);
            var imageByte = await GetImageBytes(imageFile);
            var img = Pix.LoadFromMemory(imageByte);
            var res = ocrengine.Process(img);
            return Results.Json(new { Text = res.GetText() });
        }).RequireCors("_myAllowSpecificOrigins").DisableAntiforgery();

        return app;
    }
}
