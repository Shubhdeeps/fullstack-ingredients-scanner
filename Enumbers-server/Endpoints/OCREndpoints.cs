using Enumbers_server.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Tesseract;

namespace Enumbers_server.Api.Endpoints;

public static class OCREndpoints
{
    // private static string currentDirectory = Directory.GetCurrentDirectory();
    private static string fullPath = Path.Combine(Environment.CurrentDirectory, "Data", "tessdata");

    private static string GetImageText(byte[] imageBytes)
    {
        using (var engine = new TesseractEngine(@fullPath, "eng", EngineMode.Default))
        {
            using (var img = Pix.LoadFromMemory(imageBytes))
            {
                using (var page = engine.Process(img))
                {
                    return page.GetText();
                }
            }
        }
    }

    private static async Task<byte[]> GetImageBytes(IFormFile file)
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
            var imageByte = await GetImageBytes(imageFile);
            var extractedText = GetImageText(imageByte);
            return Results.Json(new { Text = extractedText });
        }).RequireCors("_myAllowSpecificOrigins").DisableAntiforgery();

        return app;
    }
}
