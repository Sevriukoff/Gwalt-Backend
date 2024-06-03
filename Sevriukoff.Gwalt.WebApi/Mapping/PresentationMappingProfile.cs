using AutoMapper;
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
        CreateMap<TrackModel, TrackViewModel>();
    }
}