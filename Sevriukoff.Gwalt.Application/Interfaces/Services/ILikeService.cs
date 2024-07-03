using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface ILikeService
{
    Task<LikeModel?> GetAsync(LikeableType likeableType, int likeableId, int userId);

    Task<IEnumerable<LikeModel>> GetAllByUserIdAsync(int userId, LikeableType type, int pageNumber = 1,
        int pageSize = 10);
    Task<int> AddAsync(LikeableType likeableType, int likeableId, int userId);
    Task DeleteAsync(int likeId);
}