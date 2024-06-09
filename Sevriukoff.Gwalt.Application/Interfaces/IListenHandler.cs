using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IListenHandler
{
    ListenableType ListenableType { get; }
    Task<ListenModel?> GetListenAsync(int listenableId, int userId);
    Task<int> AddListenAsync(ListenModel listenModel);
}