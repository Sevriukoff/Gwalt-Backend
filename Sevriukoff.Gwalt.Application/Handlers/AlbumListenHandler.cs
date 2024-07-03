using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Handlers;

public class AlbumListenHandler : ListenHandlerBase
{
    public override ListenableType ListenableType => ListenableType.Album;

    public AlbumListenHandler(IListenRepository listenRepository) : base(listenRepository)
    {
        
    }
    
    protected override Listen CreateListen(ListenModel listenModel, int quality)
    {
        throw new NotImplementedException();
    }

    protected override int EvaluateQuality(ListenMetadata metadata, UserModel userModel)
    {
        throw new NotImplementedException();
    }

    protected override async Task IncrementListenCountAsync(int trackId)
    {
        throw new NotImplementedException();
    }

    public override async Task<ListenModel?> GetListenAsync(int trackId, int userId)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<ListenModel>> GetListensByUserIdAsync(int userId, string[]? includes, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<ListenModel>> GetListensBySessionIdAsync(string sessionId, string[]? includes, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}