using AutoMapper;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IMapper _mapper;

    public PlaylistService(IPlaylistRepository playlistRepository, IMapper mapper)
    {
        _playlistRepository = playlistRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PlaylistModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PlaylistModel> GetByIdAsync(int id)
    {
        var entity = await _playlistRepository.GetByIdAsync(id);
        var model = _mapper.Map<PlaylistModel>(entity);
        
        return model;
    }

    public async Task<int> AddAsync(PlaylistModel playlist)
    {
        return await _playlistRepository.AddAsync(_mapper.Map<Playlist>(playlist));
    }

    public async Task UpdateAsync(PlaylistModel playlist)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}