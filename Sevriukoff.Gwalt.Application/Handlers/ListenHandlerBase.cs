using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Handlers;

public abstract class ListenHandlerBase : IListenHandler
{
    public ListenableType ListenableType { get; }
    public async Task<ListenModel?> GetListenAsync(int listenableId, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddListenAsync(int listenableId, int? userId)
    {
        throw new NotImplementedException();
    }
}