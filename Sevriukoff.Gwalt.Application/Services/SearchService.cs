using AutoMapper;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class SearchService : ISearchService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly ITrackRepository _trackRepository;
    private readonly IUserRepository _userRepository;

    private readonly IMapper _mapper;

    public SearchService(IAlbumRepository albumRepository,
        ITrackRepository trackRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _albumRepository = albumRepository;
        _trackRepository = trackRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<(AlbumModel[] albumModels, TrackModel[] trackModels, UserModel[] userModels)> SearchAsync(string query)
    {
        var albums = await _albumRepository.SearchAsync(query);
        var albumModels = _mapper.Map<AlbumModel[]>(albums);

        var tracks = await _trackRepository.SearchAsync(query);
        var trackModels = _mapper.Map<TrackModel[]>(tracks);
        
        var users = await _userRepository.SearchAsync(query);
        var userModels = _mapper.Map<UserModel[]>(users);

        return (albumModels, trackModels, userModels);
    }
}