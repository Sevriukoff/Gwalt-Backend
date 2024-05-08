using System.Linq.Expressions;

namespace Sevriukoff.Gwalt.Application.Specification;

public sealed class AlternativeSpecification<T> : ComplexSpecification<T>
{
    public AlternativeSpecification(Specification<T> left, Specification<T> right) : base(left, right) { }
    
    protected override Expression<Func<T, bool>> CombineExpressions(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var paramExpr = Expression.Parameter(typeof(T));
        var exprBody = Expression.OrElse(left.Body, right.Body);
        exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
        
        var aggregateExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

        return aggregateExpr;
    }
}