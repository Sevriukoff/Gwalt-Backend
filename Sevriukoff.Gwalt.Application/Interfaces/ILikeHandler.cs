using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface ILikeHandler
{
    LikeableType LikeableType { get; }
    Task<LikeModel?> GetLikeAsync(int trackId, int userId);
    Task<int> AddLikeAsync(int likeableId, int userId);
    Task DeleteLikeAsync(int likeableId, int likeId);
}