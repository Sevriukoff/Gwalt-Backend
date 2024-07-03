using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class ListenService : IListenService
{
    private readonly IListenRepository _listenRepository;
    private readonly IDictionary<ListenableType, IListenHandler> _handlers;
    
    public ListenService(IEnumerable<IListenHandler> handlers, IListenRepository listenRepository)
    {
        _listenRepository = listenRepository;
        _handlers = handlers.ToDictionary(x => x.ListenableType, x => x);
    }
    
    public async Task<ListenModel?> GetAsync(ListenableType listenableType, int listenableId, int userId)
    {
        if (_handlers.TryGetValue(listenableType, out var handler))
        {
            return await handler.GetListenAsync(listenableId, userId);
        }
        
        throw new Exception("Handler not found");
    }
    
    public async Task<IEnumerable<ListenModel>> GetListensByUserIdAsync(ListenableType listenableType, int userId, string[]? includes, int pageNumber, int pageSize)
    {
        if (_handlers.TryGetValue(listenableType, out var handler))
        {
            return await handler.GetListensByUserIdAsync(userId, includes, pageNumber, pageSize);
        }
        
        throw new Exception("Handler not found");
    }
    
    public async Task<IEnumerable<ListenModel>> GetListensBySessionIdAsync(ListenableType listenableType, string sessionId, string[]? includes, int pageNumber, int pageSize)
    {
        if (_handlers.TryGetValue(listenableType, out var handler))
        {
            return await handler.GetListensBySessionIdAsync(sessionId, includes, pageNumber, pageSize);
        }
        
        throw new Exception("Handler not found");
    }
    
    public async Task<int> AddAsync(ListenModel listen)
    {
        if (_handlers.TryGetValue(listen.GetListenableType(), out var handler))
        {
            return await handler.AddListenAsync(listen);
        }
        
        throw new Exception("Handler not found");
    }
}