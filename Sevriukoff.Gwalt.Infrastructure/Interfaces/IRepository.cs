using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(ISpecification<T>? specification = null);
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetAsync(ISpecification<T> specification);
    Task<int> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);

    Task<int> CountAsync(ISpecification<T> specification);
}