using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IListenHandler
{
    ListenableType ListenableType { get; }
    Task<ListenModel?> GetListenAsync(int listenableId, int userId);
    Task<IEnumerable<ListenModel>> GetListensByUserIdAsync(int userId, string[]? includes, int pageNumber, int pageSize);
    Task<IEnumerable<ListenModel>> GetListensBySessionIdAsync(string sessionId, string[]? includes, int pageNumber, int pageSize);
    Task<int> AddListenAsync(ListenModel listenModel);
}