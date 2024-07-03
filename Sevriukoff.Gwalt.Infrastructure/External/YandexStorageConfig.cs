namespace Sevriukoff.Gwalt.Infrastructure.External;

public class YandexStorageConfig
{
    public required string ServiceUrl { init; get; }
    public required string BucketName { init; get; }
    public required string BaseDirectory { init; get; }
}