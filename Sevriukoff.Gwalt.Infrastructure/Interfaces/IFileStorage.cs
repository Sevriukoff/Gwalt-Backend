namespace Sevriukoff.Gwalt.Infrastructure.Interfaces;

public interface IFileStorage
{
    Task<string> UploadAsync(Stream fileStream, FileContentType contentType);
    Task<Stream> DownloadAsync(string objectName);
    Task<bool> DeleteAsync(string objectName);
}