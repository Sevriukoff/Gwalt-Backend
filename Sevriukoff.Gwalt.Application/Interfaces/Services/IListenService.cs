using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IListenService
{
    Task<ListenModel?> GetAsync(ListenableType listenableType, int listenableId, int userId);
    Task<int> AddAsync(ListenModel listenModel);
}