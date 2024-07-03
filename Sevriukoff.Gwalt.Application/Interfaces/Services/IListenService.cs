using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IListenService
{
    Task<ListenModel?> GetAsync(ListenableType listenableType, int listenableId, int userId);
    Task<IEnumerable<ListenModel>> GetListensByUserIdAsync(ListenableType listenableType, int userId, string[]? includes, int pageNumber, int pageSize);
    Task<IEnumerable<ListenModel>> GetListensBySessionIdAsync(ListenableType listenableType, string sessionId, string[]? includes, int pageNumber, int pageSize);
    Task<int> AddAsync(ListenModel listenModel);
}