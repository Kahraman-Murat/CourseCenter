using AutoMapper;
using CourseCenter.DTO.DTOs.RoleDtos;
using CourseCenter.Entity.Entities.Identity;

namespace CourseCenter.API.Mapping
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<CreateRoleDto, AppRole>().ReverseMap();
            CreateMap<UpdateRoleDto, AppRole>().ReverseMap();
            CreateMap<ResultRoleDto, AppRole>().ReverseMap();
        }
    }
}
