﻿using AutoMapper;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Specification;
using Sevriukoff.Gwalt.Application.Specification.Album;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IMapper _mapper;

    public AlbumService(IAlbumRepository albumRepository, IMapper mapper)
    {
        _albumRepository = albumRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<AlbumModel>> GetAllAsync(string[]? includes, string? orderBy, string[]? genres, int pageNumber, int pageSize)
    {
        var includeSpec = new IncludingSpecification<Album>(includes);
        var orderSpec = new SortingSpecification<Album>(orderBy);
        var genreSpec = new AlbumsByGenresSpecification(genres);
        var compositeSpec = includeSpec
            .And(orderSpec)
            .And(genreSpec);
        
        var albums = await _albumRepository.GetAllAsync(pageNumber, pageSize, compositeSpec);
        var albumModels = _mapper.Map<IEnumerable<AlbumModel>>(albums);
        
        return albumModels;
    }

    public async Task<AlbumModel> GetByIdAsync(int id, string[]? includes)
    {
        var includeSpec = new IncludingSpecification<Album>("Genre");
        var album = await _albumRepository.GetByIdAsync(id, includeSpec);
        var albumModel = _mapper.Map<AlbumModel>(album);
        
        return albumModel;
    }

    public async Task<int> AddAsync(AlbumModel album)
    {
        var albumEntity = _mapper.Map<Album>(album);
        var albumId = await _albumRepository.AddAsync(albumEntity);
        
        return albumId;
    }
}