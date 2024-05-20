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
        CreateMap<AlbumModel, AlbumViewModel>()
            .ForMember(x => x.CoverUrl, opt => opt.MapFrom(src => src.ImageUrl));
        CreateMap<TrackModel, TrackViewModel>();
    }
}