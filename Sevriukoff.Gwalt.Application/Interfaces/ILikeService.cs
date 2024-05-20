using Sevriukoff.Gwalt.Application.Enums;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface ILikeService
{
    Task<int> AddAsync(LikeableType likeableType, int likeableId, int userId);
    Task DeleteAsync(int likeId);
}