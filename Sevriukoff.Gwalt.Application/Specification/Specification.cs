using System.Linq.Expressions;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Specification;

public abstract class Specification<T> : ISpecification<T>
{
    public virtual Expression<Func<T, bool>>? FilterCondition { get; set; }
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public Expression<Func<T, object>>? GroupBy { get; private set; }
    public List<Expression<Func<T, object>>> Includes => _includeCollection;
    private readonly List<Expression<Func<T, object>>> _includeCollection = new();
    public List<string> IncludeStrings { get; } = new();

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
        => Includes.Add(includeExpression);

    protected void AddInclude(string includeString)
        => IncludeStrings.Add(includeString);

    protected void ApplyOrderBy(Expression<Func<T, object>>? orderByExpression)
        => OrderBy = orderByExpression;

    protected void ApplyOrderByDescending(Expression<Func<T, object>>? orderByDescendingExpression)
        => OrderByDescending = orderByDescendingExpression;

    protected void ApplySort(string propertyPath, bool descending)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var body = propertyPath.Split('.').Aggregate((Expression) parameter, Expression.Property);

        if (body.Type.IsValueType)
            body = Expression.Convert(body, typeof(object));

        var selector = Expression.Lambda<Func<T, object>>(body, parameter);

        if (descending)
            ApplyOrderByDescending(selector);
        else
            ApplyOrderBy(selector);
    }

    protected void SetFilterCondition(Expression<Func<T, bool>>? filterExpression)
        => FilterCondition = filterExpression;

    protected void ApplyGroupBy(Expression<Func<T, object>>? groupByExpression)
        => GroupBy = groupByExpression;

    internal Specification<T> And(Specification<T> specification)
        => new CompositeSpecification<T>(this, specification);

    public Specification<T> Or(Specification<T> specification)
        => new AlternativeSpecification<T>(this, specification);

    public bool IsSatisfiedBy(T entity)
        => FilterCondition?.Compile()(entity) ?? throw new NullReferenceException(nameof(FilterCondition));
}