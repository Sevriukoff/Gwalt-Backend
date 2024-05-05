namespace Sevriukoff.Gwalt.Infrastructure.Interfaces;

public interface IFileStorage
{
    Task<string> UploadAsync(Stream fileStream, FileContentType contentType);
}