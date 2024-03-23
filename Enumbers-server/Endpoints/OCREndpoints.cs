using Enumbers_server.Api.Dtos;
using Tesseract;

namespace Enumbers_server.Api.Endpoints;

public static class OCREndpoints
{
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
        app.MapPost("/recognize", async (OCRDto ImageDto) =>
        {
            var imageFile = ImageDto.ImageFile;
            var ocrengine = new TesseractEngine(@".\tessdata", "eng", EngineMode.Default);
            var imageByte = await GetImageBytes(imageFile);
            var img = Pix.LoadFromMemory(imageByte);
            var res = ocrengine.Process(img);
            return Results.Json(new { Text = res.GetText() });
        });

        return app;
    }
}
