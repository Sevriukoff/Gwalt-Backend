namespace Sevriukoff.Gwalt.Application.Specification.Listen;

public class ListensByUserSpecification : Specification<Infrastructure.Entities.Listen>
{
    public ListensByUserSpecification(int? userId)
    {
        if (userId == null)
            return;
        
        SetFilterCondition(x => x.UserId == userId);
    }
}