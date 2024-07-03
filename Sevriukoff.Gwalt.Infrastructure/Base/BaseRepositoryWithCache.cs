using Sevriukoff.Gwalt.Infrastructure.Caching;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Base;

public class BaseRepositoryWithCache<T> : IRepository<T> where T : BaseEntity
{
    private readonly IRepository<T> _repository;
    protected readonly ICacheApplier<T> CacheApplier;

    public BaseRepositoryWithCache(IRepository<T> repository, ICacheApplier<T> cacheApplier)
    {
        _repository = repository;
        CacheApplier = cacheApplier;
    }

    public async Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10, ISpecification<T>? specification = null)
    {
        var entities = (await _repository.GetAllAsync(pageNumber, pageSize, specification)).ToList();
        await CacheApplier.ApplyCacheAsync(entities);
        return entities;
    }

    public async Task<T?> GetByIdAsync(int id, ISpecification<T>? specification = null)
    {
        var entity = await _repository.GetByIdAsync(id, specification);
        
        if (entity == null)
            return entity;
        
        await CacheApplier.ApplyCacheAsync(entity);
        return entity;
    }

    public async Task<T?> GetAsync(ISpecification<T> specification)
    {
        var entity = await _repository.GetAsync(specification);
        
        if (entity == null)
            return entity;
        
        await CacheApplier.ApplyCacheAsync(entity);
        return entity;
    }

    public async Task<int> AddAsync(T entity)
        => await _repository.AddAsync(entity);

    public async Task<bool> UpdateAsync(T entity)
        => await _repository.UpdateAsync(entity);

    public async Task<bool> DeleteAsync(int id)
        => await _repository.DeleteAsync(id);

    public async Task<bool> IsExistsAsync(int id)
        => await _repository.IsExistsAsync(id);

    public async Task<int> CountAsync(ISpecification<T> specification)
        => await _repository.CountAsync(specification);

    public IQueryable<T> GetQueryByIdAsync(int id)
        => _repository.GetQueryByIdAsync(id);
}