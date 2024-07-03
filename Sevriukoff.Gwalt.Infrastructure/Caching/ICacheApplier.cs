namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public interface ICacheApplier<in T>
{
    Task ApplyCacheAsync(T? entity);
    Task ApplyCacheAsync(IEnumerable<T>? entity);
}