using Sevriukoff.Gwalt.Application.Enums;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface ILikeHandler
{
    LikeableType LikeableType { get; }
    Task<int> AddLikeAsync(int likeableId, int userId);
    Task DeleteLikeAsync(int likeableId, int likeId);
}
