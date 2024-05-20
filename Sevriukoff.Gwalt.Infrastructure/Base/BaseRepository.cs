using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Base;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly DataDbContext Context;

    protected BaseRepository(DataDbContext context)
    {
        Context = context;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T>? specification = null)
    {
        if (specification != null)
            return await SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), specification)
                .ToListAsync();
        
        return await Context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id, ISpecification<T>? specification = null)
    {
        if (specification != null)
            return await SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), specification)
                .SingleAsync(x => x.Id == id);
        
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetAsync(ISpecification<T> specification)
        => await SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), specification)
            .FirstOrDefaultAsync();

    public async Task<int> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity.Id;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        return await Context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await Context.Set<T>().FindAsync(id);
        
        if (entity == null)
            return false;
        
        Context.Set<T>().Remove(entity);
        return await Context.SaveChangesAsync() > 0;
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
        => await SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), specification)
            .CountAsync();
}