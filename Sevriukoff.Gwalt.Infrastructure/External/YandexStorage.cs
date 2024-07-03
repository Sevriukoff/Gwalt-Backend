using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.External;

public class YandexStorage : IFileStorage
{
    private readonly IAmazonS3 _storageClient;
    private readonly YandexStorageConfig _config;
    
    public YandexStorage(IAmazonS3 storageClient, IOptions<YandexStorageConfig> config)
    {
        _storageClient = storageClient;
        _config = config.Value;
    }

    public async Task<string> UploadAsync(Stream fileStream, FileContentType contentType)
    {
        var fileName = GenerateFileName(contentType);
        var filePath = GetDirectoryByType(contentType);
        
        var request = new PutObjectRequest
        {
            BucketName = _config.BucketName,
            Key = string.Concat(filePath, fileName),
            ContentType = contentType.ToString(),
            InputStream = fileStream,
        };

        var response = await _storageClient.PutObjectAsync(request);
        
        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        {
            return Path.Combine(_config.BaseDirectory, filePath, fileName);
        }
        
        return string.Empty;
    }

    public async Task<Stream> DownloadAsync(string objectName)
    {
        var request = new GetObjectRequest
        {
            BucketName = _config.BucketName,
            Key = objectName.Replace(_config.BaseDirectory, ""),
        };
    
        var response = await _storageClient.GetObjectAsync(request);
        
        var memoryStream = new MemoryStream();
        await response.ResponseStream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        
        return memoryStream;
    }

    public async Task<bool> DeleteAsync(string objectName)
    {
        try
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _config.BucketName,
                Key = objectName,
            };
            
            await _storageClient.DeleteObjectAsync(deleteObjectRequest);

            return true;
        }
        catch (AmazonS3Exception ex)
        {
            return false;
        }
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