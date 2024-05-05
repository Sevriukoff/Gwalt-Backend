using Amazon.S3;
using Amazon.S3.Model;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure;

public class YandexStorage : IFileStorage
{
    private readonly string _bucketName;
    private readonly IAmazonS3 _storageClient;
    
    public YandexStorage(IAmazonS3 storageClient, string bucketName = "id-gwalt-storage")
    {
        _storageClient = storageClient;
        _bucketName = bucketName;
    }

    public async Task<string> UploadAsync(Stream fileStream, FileContentType contentType)
    {
        var fileName = GenerateFileName(contentType);
        var filePath = GetDirectoryByType(contentType);
        
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = string.Concat(filePath, fileName),
            ContentType = contentType.ToString(),
            InputStream = fileStream,
        };

        var response = await _storageClient.PutObjectAsync(request);
        
        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        {
            return fileName;
        }
        
        return string.Empty;
    }

    private string GenerateFileName(FileContentType type)
    {
        var random = new Random();
        
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        var randomPart = random.Next(0, 999999);
        var seed = Guid.NewGuid().ToString();
        
        var uniqueName = $"{seed}-{timestamp}-{randomPart}.{type.FileExtension}";
        
        return uniqueName;
    }
    
    private string GetDirectoryByType(FileContentType type)
    {
        return type.FileType switch
        {
            "image" => "image/",
            "audio" => "audio/",
            _ => "other/"
        };
    }
}