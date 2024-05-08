namespace Sevriukoff.Gwalt.Application.Specification;

public class IncludingSpecification<T> : Specification<T>
{
    public IncludingSpecification(params string[] includes)
    {
        foreach (var include in includes)
            AddInclude(include);
    }
}