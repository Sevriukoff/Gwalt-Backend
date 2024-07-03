namespace Sevriukoff.Gwalt.Application.Specification.Listen;

public class ListensBySessionIdSpecification : Specification<Infrastructure.Entities.Listen>
{
    public ListensBySessionIdSpecification(string sessionId)
    {
        SetFilterCondition(x => x.SessionId == sessionId);
    }
}