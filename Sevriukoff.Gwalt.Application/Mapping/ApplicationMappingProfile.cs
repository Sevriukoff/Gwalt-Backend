using AutoMapper;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Application.Mapping;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        #region Mappings Model <=> Entity
        
        CreateMap<User, UserModel>()
            .ReverseMap();
        
        CreateMap<UserModel, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
        CreateMap<Like, LikeModel>()
                .ForMember(dest => dest.Likeable, opt => 
                    opt.MapFrom<ILikeable>
                    (
                        (likeEntity, likeModel, likeable, context) =>
                        {
                            if (likeEntity.TrackId.HasValue)
                            {
                                return context.Mapper.Map<TrackModel>(likeEntity.Track);
                            }
                            
                            if (likeEntity.AlbumId.HasValue)
                            {
                                return context.Mapper.Map<AlbumModel>(likeEntity.Album);
                            }
                            
                            return null;
                        }
                    )
                );
            
        CreateMap<Album, AlbumModel>()
            .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.ListensCount, opt => opt.MapFrom(src => src.ListensCount))
            .ReverseMap();

        CreateMap<Genre, GenreModel>()
            .ReverseMap();

        CreateMap<Track, TrackModel>()
            .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album != null ? src.Album : new Album { Id = src.AlbumId }))
            .ForMember(dest => dest.Peaks, opt => opt.MapFrom(src => src.Peaks.Peaks));

        CreateMap<TrackModel, Track>()
            .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.Album.Id))
            .ForMember(dest => dest.Album, opt => opt.Ignore());
            
        CreateMap<Genre, GenreModel>()
            .ReverseMap();
        
        #endregion
    }
}