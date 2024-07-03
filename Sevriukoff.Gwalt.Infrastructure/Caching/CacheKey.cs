namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public class CacheKey
{
    public string InstanceName { get; }
    public Type EntityType { get; }
    public string EntityId { get; }
    public CacheKeyType KeyType { get; }

    public CacheKey(string instanceName, Type entityType, string entityId, CacheKeyType keyType)
    {
        InstanceName = instanceName;
        EntityType = entityType;
        EntityId = entityId;
        KeyType = keyType;
    }

    public CacheKey(string rowKey)
    {
        var parts = rowKey.Split(':');
        
        if (parts.Length != 4)
            throw new ArgumentException($"Invalid row key: {rowKey}");

        InstanceName = parts[0].Trim('[', ']');
        EntityType = Type.GetType(parts[1])!;
        EntityId = parts[2];
        KeyType = (CacheKeyType) Enum.Parse(typeof(CacheKeyType), parts[3]);
    }
        
    public string GetKey() => ToString();

    //"[Gwalt]:Sevriukoff.Gwalt.Infrastructure.Entities.Track:1:ListensCount"
    public override string ToString()
        => $"[{InstanceName}]:{EntityType.FullName}:{EntityId}:{KeyType}";
}