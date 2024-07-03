using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Services;
using Sevriukoff.Gwalt.Application.Specification.Like;
using Sevriukoff.Gwalt.Infrastructure.Caching;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Handlers;

public class AlbumLikeHandler : LikeHandlerBase
{
    private readonly IAlbumRepository _albumRepository;
    private readonly LikeCacheClient _likeCacheClient;
    public override LikeableType LikeableType => LikeableType.Album;

    public AlbumLikeHandler(ILikeRepository likeRepository, IAlbumRepository albumRepository, LikeCacheClient likeCacheClient) : base(likeRepository)
    {
        _albumRepository = albumRepository;
        _likeCacheClient = likeCacheClient;
    }

    protected override Like CreateLike(int likeableId, int userId)
    {
        return new Like
        {
            AlbumId = likeableId,
            UserId = userId,
            ReleaseDate = DateTime.UtcNow
        };
    }

    protected override async Task IncrementLikeCountAsync(int likeableId)
    {
        var authorsIds = await _albumRepository.GetAuthorsIds(likeableId);

        await _likeCacheClient.IncrementAlbumLikesAsync(likeableId);
        
        foreach (var authorId in authorsIds)
        {
            await _likeCacheClient.IncrementUserLikeAsync(authorId);
        }
    }

    protected override async Task DecrementLikeCountAsync(int likeableId)
    {
        var authorsIds = await _albumRepository.GetAuthorsIds(likeableId);

        await _likeCacheClient.DecrementAlbumLikesAsync(likeableId);
        
        foreach (var authorId in authorsIds)
        {
            await _likeCacheClient.DecrementUserLikeAsync(authorId);
        }
    }

    protected override async Task<bool> IsExists(Like like)
    {
        var albumId = like.AlbumId.Value;
        var userId = like.UserId;

        var spec = new LikeByUserAndAlbumSpecification(userId, albumId);
        var likeEntity = await LikeRepository.GetAsync(spec);

        return likeEntity != null;
    }

    public override async Task<LikeModel?> GetAsync(int albumId, int userId)
    {
        var spec = new LikeByUserAndAlbumSpecification(userId, albumId);
        var like = await LikeRepository.GetAsync(spec);
        
        if (like == null)
            return null;
        
        return new LikeModel
        {
            Id = like.Id,
            Likeable = new AlbumModel
            {
                Id = albumId
            },
            ReleaseDate = like.ReleaseDate
        };
    }

    public override async Task<IEnumerable<LikeModel>> GetAllByUserIdAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var spec = new LikesByUserOnAlbumsSpecification(userId);
        var likes = await LikeRepository.GetAllAsync(pageNumber, pageSize, spec);
        
        return likes.Select(x => new LikeModel
        {
            Id = x.Id,
            Likeable = new AlbumModel()
            {
                Id = x.AlbumId!.Value
            },
            ReleaseDate = x.ReleaseDate
        });
    }
}