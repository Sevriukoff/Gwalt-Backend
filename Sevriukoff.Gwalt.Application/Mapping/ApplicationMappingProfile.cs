using AutoMapper;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Application.Mapping;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<User, UserModel>();
            
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
            
        CreateMap<Album, AlbumModel>();

        CreateMap<Track, TrackModel>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.TotalLikes.Count));
            
        CreateMap<Genre, GenreModel>()
            .ReverseMap();
        
        #endregion
    }
}