using AutoMapper;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Mapping;

public class PresentationMappingProfile : Profile
{
    
    public PresentationMappingProfile()
    {
        CreateMap<UserModel, UserWithStatViewModel>();
        
        CreateMap<UserModel, UserViewModel>();
        CreateMap<AlbumModel, AlbumViewModel>();
        CreateMap<AlbumCreateViewModel, AlbumModel>();
        CreateMap<TrackCreateViewModel, TrackModel>()
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.Duration)))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(x => new GenreModel() {Id = x})))
            .ForMember(dest => dest.Album, opt => opt.MapFrom(src => new AlbumModel() {Id = src.AlbumId}));
        CreateMap<GenreModel, GenreViewModel>();
        CreateMap<TrackModel, TrackViewModel>()
            .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.Album.CoverUrl))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Album.Authors));
        
        CreateMap<ListenCreateViewModel, ListenModel>()
            .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => new ListenMetadata
            {
                ActiveListeningTime = TimeSpan.FromSeconds(src.ActiveListeningTime),
                EndTime = TimeSpan.FromSeconds(src.EndTime),
                TotalDuration = TimeSpan.FromSeconds(src.TotalDuration),
                SeekCount = src.SeekCount,
                PauseCount = src.PauseCount,
                Volume = src.Volume
            }));

        CreateMap<ListenCreateViewModel, TrackModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ListenableId));

        CreateMap<ListenCreateViewModel, AlbumModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ListenableId));
    }
}