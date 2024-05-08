using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Base;

internal class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecification<TEntity>? specifications)
    {
        if (specifications == null)
            return query;
        
        if (specifications.FilterCondition != null)
            query = query.Where(specifications.FilterCondition);

        query = specifications.Includes
            .Aggregate(query, (current, include) => current.Include(include));
        
        query = specifications.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specifications.OrderBy != null)
            query = query.OrderBy(specifications.OrderBy);
        else if (specifications.OrderByDescending != null)
            query = query.OrderByDescending(specifications.OrderByDescending);

        if (specifications.GroupBy != null)
            query = query.GroupBy(specifications.GroupBy).SelectMany(x => x);

        return query;
    }
}