using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Handlers;

public abstract class LikeHandlerBase : ILikeHandler
{
    public abstract LikeableType LikeableType { get; }
    protected readonly ILikeRepository LikeRepository;

    protected LikeHandlerBase(ILikeRepository likeRepository)
    {
        LikeRepository = likeRepository;
    }
    
    protected abstract Like CreateLike(int likeableId, int userId);
    protected abstract Task IncrementLikeCountAsync(int likeableId);
    protected abstract Task DecrementLikeCountAsync(int likeableId);
    protected abstract Task<bool> IsExists(Like like);
    public abstract Task<LikeModel?> GetAsync(int trackId, int userId);
    public abstract Task<IEnumerable<LikeModel>> GetAllByUserIdAsync(int userId, int pageNumber = 1, int pageSize = 10);

    public async Task<int> AddAsync(int likeableId, int userId)
    {
        var like = CreateLike(likeableId, userId);
        var likeIsExists = await IsExists(like);
        
        if (likeIsExists)
        {
            throw new Exception("Like already exists");
        }
        
        var id = await LikeRepository.AddAsync(like);
        await IncrementLikeCountAsync(likeableId);

        return id;
    }


    public async Task DeleteAsync(int likeableId, int likeId)
    {
        await LikeRepository.DeleteAsync(likeId);
        await DecrementLikeCountAsync(likeableId);
    }
}