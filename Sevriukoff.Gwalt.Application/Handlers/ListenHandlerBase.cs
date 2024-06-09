using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Handlers;

public abstract class ListenHandlerBase : IListenHandler
{
    public abstract ListenableType ListenableType { get; }
    protected readonly IListenRepository ListenRepository;

    protected ListenHandlerBase(IListenRepository listenRepository)
    {
        ListenRepository = listenRepository;
    }
    
    protected abstract Listen CreateListen(ListenModel listenModel, int quality);
    protected abstract int EvaluateQuality(ListenMetadata metadata, UserModel userModel);
    protected abstract Task IncrementListenCountAsync(int trackId);
    public abstract Task<ListenModel?> GetListenAsync(int trackId, int userId);

    public async Task<int> AddListenAsync(ListenModel listenModel)
    {
        var quality = EvaluateQuality(listenModel.Metadata, listenModel.User);
        var listen = CreateListen(listenModel, quality);
        var id = await ListenRepository.AddAsync(listen);
        await IncrementListenCountAsync(listen.TrackId.Value);
        
        return id;
    }
}