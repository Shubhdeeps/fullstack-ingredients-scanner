namespace Enumbers_server.Api.Dtos;

public record class OCRDto
(
    IFormFile ImageFile,
    string ImageText
);
