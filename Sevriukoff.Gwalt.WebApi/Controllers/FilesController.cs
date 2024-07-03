using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.External;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly ImageProcessor _imageProcessor;
    
    public FilesController(IFileService fileService, ImageProcessor imageProcessor)
    {
        _fileService = fileService;
        _imageProcessor = imageProcessor;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromQuery] int x, [FromQuery] int y, [FromQuery] int width, [FromQuery] int height)
    {
        try
        {
            var contentType = Request.ContentType;

            if (contentType == null)
                return BadRequest("Content type is missing");

            using (var memoryStream = new MemoryStream())
            {
                await Request.Body.CopyToAsync(memoryStream);

                if (contentType.StartsWith("image/") && x != 0 && y != 0 && width != 0 && height != 0)
                {
                    await using var croppedImage = await _imageProcessor.CropImageAsync(memoryStream, x, y, width, height );
                    var fileId = await _fileService.UploadImageAsync(croppedImage, contentType);
                    
                    return CreatedAtAction(nameof(Get),new { id = fileId }, new {id = fileId});
                }
                else
                {
                    var fileId = await _fileService.UploadImageAsync(memoryStream, contentType);
                    return CreatedAtAction(nameof(Get),new { id = fileId }, new { id = fileId});
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Error processing binary file: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        //https://storage.yandexcloud.net/id-gwalt-storage/image/415c7def-1176-410a-a431-a832591025dc-20240618200236-182075.png
        
        return Ok();
    }
}