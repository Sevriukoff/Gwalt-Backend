using System.Linq.Expressions;

namespace Sevriukoff.Gwalt.Application.Specification;

public abstract class ComplexSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    protected ComplexSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;

        IncludeStrings.AddRange(_left.IncludeStrings);
        IncludeStrings.AddRange(_right.IncludeStrings);

        if (_left.OrderBy != null && _right.OrderBy != null)
            ApplyOrderBy(_right.OrderBy);
        else if (_left.OrderByDescending != null && _right.OrderByDescending != null)
            ApplyOrderByDescending(_right.OrderByDescending);

        if (_left.OrderBy == null || _right.OrderBy == null)
            ApplyOrderBy(_left.OrderBy ?? _right.OrderBy);

        if (_left.OrderByDescending == null || _right.OrderByDescending == null)
            ApplyOrderByDescending(_left.OrderByDescending ?? _right.OrderByDescending);
    }

    public override Expression<Func<T, bool>>? FilterCondition => GetFilterExpression();

    protected virtual Expression<Func<T, bool>>? GetFilterExpression()
    {
        var leftExpression = _left.FilterCondition;
        var rightExpression = _right.FilterCondition;

        if (leftExpression == null || rightExpression == null)
            return leftExpression ?? rightExpression;

        return CombineExpressions(leftExpression, rightExpression);
    }

    protected abstract Expression<Func<T, bool>> CombineExpressions(Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right);
}
