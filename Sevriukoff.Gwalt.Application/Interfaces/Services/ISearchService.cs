using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Services;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface ISearchService
{
    Task<(AlbumModel[] albumModels, TrackModel[] trackModels, UserModel[] userModels)> SearchAsync(string query);
}