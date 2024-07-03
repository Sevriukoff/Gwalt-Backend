namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public class RedisConfig
{
    public required string InstanceName { get; set; }
    public required int UpdateInterval { get; set; }
}