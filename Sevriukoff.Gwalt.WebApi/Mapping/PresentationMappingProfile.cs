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
    }
}