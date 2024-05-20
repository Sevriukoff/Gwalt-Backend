using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Specification.Like;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class AlbumLikeHandler : LikeHandlerBase
{
    private readonly IAlbumRepository _albumRepository;
    public override LikeableType LikeableType => LikeableType.Album;

    public AlbumLikeHandler(ILikeRepository likeRepository, IAlbumRepository albumRepository) : base(likeRepository)
    {
        _albumRepository = albumRepository;
    }

    protected override Like CreateLike(int likeableId, int userId)
    {
        return new Like
        {
            AlbumId = likeableId,
            LikeById = userId,
            ReleaseDate = DateTime.UtcNow
        };
    }

    protected override async Task IncrementLikeCountAsync(int likeableId)
    {
        var album = await _albumRepository.GetByIdAsync(likeableId);
        album.LikeCount++;
        await _albumRepository.UpdateAsync(album);
    }

    protected override async Task DecrementLikeCountAsync(int likeableId)
    {
        var album = await _albumRepository.GetByIdAsync(likeableId);
        album.LikeCount--;
        await _albumRepository.UpdateAsync(album);
    }

    protected override async Task<bool> IsExists(Like like)
    {
        var albumId = like.AlbumId.Value;
        var userId = like.LikeById;

        var spec = new LikeOnAlbumSpecification(userId, albumId);
        var likeEntity = await _likeRepository.GetAsync(spec);

        return likeEntity != null;
    }
}