using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Infrastructure;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class FileService : IFileService
{
    private readonly IFileStorage _fileStorage;
    
    public FileService(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    public async Task<string> UploadImageAsync(Stream image, string contentType)
    {
        return await _fileStorage.UploadAsync(image, new FileContentType(contentType));
    }
}