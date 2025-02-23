using AutoMapper;
using CourseCenter.DTO.DTOs.UserDtos;
using CourseCenter.Entity.Entities.Identity;

namespace CourseCenter.API.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<CreateUserDto, AppUser>().ReverseMap();
            CreateMap<UpdateUserDto, AppUser>().ReverseMap();
            CreateMap<ResultUserDto, AppUser>().ReverseMap();
            CreateMap<ResultUserSocialMediasDto, AppUser>().ReverseMap();
        }
    }
}
