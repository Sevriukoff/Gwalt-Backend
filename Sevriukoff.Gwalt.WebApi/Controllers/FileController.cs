using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Interfaces;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    
    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        try
        {
            var contentType = Request.ContentType;

            using (var memoryStream = new MemoryStream())
            {
                await Request.Body.CopyToAsync(memoryStream);
                
                var fileId = await _fileService.UploadImageAsync(memoryStream, contentType);

                return CreatedAtAction(nameof(Get),new { id = fileId }, fileId);
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Error processing binary file: " + ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return Ok();
    }
}