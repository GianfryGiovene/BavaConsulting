using AutoMapper;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.User;

namespace CleanArchitecture.ApplicationCore.MapperProfile;

public sealed class MapperProfile : Profile
{
    public MapperProfile() 
    {
        this.UserMapper();
    }

    private void UserMapper()
    {
        CreateMap<User, UserReadVM>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FiscalCode, opt => opt.MapFrom(src => src.FiscalCode))
            .ReverseMap();            
    }
}
