using System.Linq.Expressions;

namespace Sevriukoff.Gwalt.Infrastructure.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? FilterCondition { get; set; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>>? GroupBy { get; }
    
    bool IsSatisfiedBy(T entity);
}