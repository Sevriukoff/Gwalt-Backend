using Sevriukoff.Gwalt.Infrastructure;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IFileService
{
    Task<string> UploadImageAsync(Stream image, string contentType);
    Task<Stream> DownloadAsync(string objectName);
    Task<bool> DeleteAsync(string objectName);
}