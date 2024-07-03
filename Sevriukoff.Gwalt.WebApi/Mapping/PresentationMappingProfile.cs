using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.WebApi.Controllers.Users;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Mapping;

public class PresentationMappingProfile : Profile
{
    
    public PresentationMappingProfile()
    {
        UserMappings();
        TrackMappings();
        AlbumMappings();
        ListenMappings();

        CreateMap<LikeModel, LikeTrackViewModel>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Likeable.Id))
            .ForMember(x => x.CoverUrl, opt => opt.MapFrom(src => (src.Likeable as TrackModel).Album.CoverUrl))
            .ForMember(x => x.Authors, opt => opt.MapFrom(src => (src.Likeable as TrackModel).Album.Authors.Select(x => new LikeTrackViewModel.AuthorViewModel(){Id = x.Id, Name = x.Name})))
            .ForMember(x => x.Title, opt => opt.MapFrom(src => (src.Likeable as TrackModel).Title))
            .ForMember(x => x.LikesCount, opt => opt.MapFrom(src => (src.Likeable as TrackModel).LikesCount))
            .ForMember(x => x.ListensCount, opt => opt.MapFrom(src => (src.Likeable as TrackModel).ListensCount));
        
        CreateMap<GenreModel, GenreViewModel>();
        
        CreateMap<GenreModel, string>()
            .ConvertUsing(src => src.Name);
        
        CreateMap<int, GenreModel>()
            .ConvertUsing(src => new GenreModel {Id = src});
    }
    
    private void UserMappings()
    {
        CreateMap<UserModel, UserWithStatViewModel>();
        
        CreateMap<UserModel, UserViewModel>();

        CreateMap<UserRegisterViewModel, UserModel>();

        CreateMap<UserUpdateViewModel, UserModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<UserModel, UserUpdateViewModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<UserModel, TrackAuthorsViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<UserModel, UserFloatViewModel>();
    }
    
    private void TrackMappings()
    {
        CreateMap<TrackModel, TrackViewModel>()
            .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.Album.CoverUrl))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Album.Authors))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.TotalSeconds));
        
        CreateMap<TrackModel, TrackFloatViewModel>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(x => x.Name)))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Album.Authors))
            .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.Album.CoverUrl))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.TotalSeconds))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.Album.ReleaseDate));
        
        CreateMap<TrackCreateViewModel, TrackModel>()
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.Duration)))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(x => new GenreModel() {Id = x})))
            .ForMember(dest => dest.Album, opt => opt.MapFrom(src => new AlbumModel() {Id = src.AlbumId}));
        
        CreateMap<TrackModel, TrackSearchViewModel>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Album.Authors.Select(x => x.Name)))
            .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.Album.CoverUrl));
    }
    
    private void AlbumMappings()
    {
        CreateMap<AlbumModel, AlbumViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
        
        CreateMap<AlbumCreateViewModel, AlbumModel>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.AuthorsIds.Select(x => new UserModel {Id = x})))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.Duration)))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.GenreId));
        
        CreateMap<AlbumModel, AlbumSearchViewModel>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(x => x.Name)));

        CreateMap<AlbumModel, AlbumGetByIdViewModel>()
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.TotalSeconds));
    }
    
    private void ListenMappings()
    {
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
        
        CreateMap<ListenModel, ListenTrackViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (src.Listenable as TrackModel).Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => (src.Listenable as TrackModel).Title))
            .ForMember(dest => dest.IsExplicit, opt => opt.MapFrom(src => (src.Listenable as TrackModel).IsExplicit))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => (src.Listenable as TrackModel).Genres.Select(x => x.Name)))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => (src.Listenable as TrackModel).Album.Authors))
            .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => (src.Listenable as TrackModel).LikesCount))
            .ForMember(dest => dest.ListensCount, opt => opt.MapFrom(src => (src.Listenable as TrackModel).ListensCount))
            .ForMember(dest => dest.SharesCount, opt => opt.MapFrom(src => (src.Listenable as TrackModel).SharesCount))
            .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => (src.Listenable as TrackModel).Album.CoverUrl))
            .ForMember(dest => dest.ListenPercent, opt => opt.MapFrom(src => (src.Metadata.EndTime.TotalSeconds / src.Metadata.TotalDuration.TotalSeconds) * 100))
            .ForMember(dest => dest.ListenTrackDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.ListenQuality, opt => opt.MapFrom(src => src.Metadata.Quality));
    }
}