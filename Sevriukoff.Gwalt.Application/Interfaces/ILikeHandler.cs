using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface ILikeHandler
{
    LikeableType LikeableType { get; }
    Task<LikeModel?> GetAsync(int trackId, int userId);
    Task<IEnumerable<LikeModel>> GetAllByUserIdAsync(int userId, int pageNumber = 1, int pageSize = 10);
    Task<int> AddAsync(int likeableId, int userId);
    Task DeleteAsync(int likeableId, int likeId);
}