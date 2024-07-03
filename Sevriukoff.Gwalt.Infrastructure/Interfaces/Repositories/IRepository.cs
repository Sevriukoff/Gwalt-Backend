using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10, ISpecification<T>? specification = null);
    Task<T?> GetByIdAsync(int id, ISpecification<T>? specification = null);
    Task<T?> GetAsync(ISpecification<T> specification);
    Task<int> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    
    Task<bool> IsExistsAsync(int id);
    Task<int> CountAsync(ISpecification<T> specification);
    
    internal IQueryable<T> GetQueryByIdAsync(int id);
}