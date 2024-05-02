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
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task<int> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(T entity)
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
}