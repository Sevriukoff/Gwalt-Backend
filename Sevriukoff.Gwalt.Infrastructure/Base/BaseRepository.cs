using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Base;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly DataDbContext Context;

    protected BaseRepository(DataDbContext context)
    {
        Context = context;
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10, ISpecification<T>? specification = null)
    {
        var query = Context.Set<T>().AsQueryable();
        
        if (specification != null)
            query = SpecificationEvaluator<T>.GetQuery(query, specification);

        return await PaginatedList<T>.CreateAsync(query, pageNumber, pageSize);
    }

    public virtual async Task<T?> GetByIdAsync(int id, ISpecification<T>? specification = null)
    {
        if (specification != null)
            return await SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), specification)
                .SingleAsync(x => x.Id == id);
        
        return await Context.Set<T>().FindAsync(id);
    }

    public virtual async Task<T?> GetAsync(ISpecification<T> specification)
        => await SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), specification)
            .FirstOrDefaultAsync();

    public virtual async Task<int> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity.Id;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        return await Context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await Context.Set<T>().FindAsync(id);
        
        if (entity == null)
            return false;
        
        Context.Set<T>().Remove(entity);
        return await Context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> IsExistsAsync(int id)
        => await Context.Set<T>().AnyAsync(x => x.Id == id);

    public async Task<int> CountAsync(ISpecification<T> specification)
        => await SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), specification)
            .CountAsync();

    IQueryable<T> IRepository<T>.GetQueryByIdAsync(int id)
    {
        return Context.Set<T>().Where(x => x.Id == id);
    }
    
    protected virtual async Task IncrementFieldAsync(string columnName, int entityId, int increment)
    {
        string tableName = typeof(T).Name + "s";
        
        string sql = $"UPDATE \"{tableName}\" SET \"{columnName}\" = \"{columnName}\" + @increment WHERE \"Id\" = @entityId";
        var parameters = new[] {
            new NpgsqlParameter("@increment", increment),
            new NpgsqlParameter("@entityId", entityId)
        };

        await Context.Database.ExecuteSqlRawAsync(sql, parameters);
    }
    
    protected virtual async Task<IEnumerable<T>> GetFilteredAsync(
        Expression<Func<T, bool>> filter, 
        int pageNumber, 
        int pageSize, 
        ISpecification<T>? spec = null)
    {
        var baseQuery = Context.Set<T>().Where(filter);
        var specQuery = SpecificationEvaluator<T>.GetQuery(baseQuery, spec);
        var paginatedList = await PaginatedList<T>.CreateAsync(specQuery, pageNumber, pageSize);

        return paginatedList;
    }
    
    protected virtual async Task<IEnumerable<T>> GetFilteredAsync(
        IQueryable<T> filter, 
        int pageNumber, 
        int pageSize, 
        ISpecification<T>? spec = null)
    {
        var specQuery = SpecificationEvaluator<T>.GetQuery(filter, spec);
        var paginatedList = await PaginatedList<T>.CreateAsync(specQuery, pageNumber, pageSize);

        return paginatedList;
    }
}