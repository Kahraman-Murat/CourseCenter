using AutoMapper;
using CourseCenter.DTO.DTOs.CourseRegisterDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class CourseRegisterMapping : Profile
    {
        public CourseRegisterMapping()
        {
            CreateMap<CreateCourseRegisterDto, CourseRegister>().ReverseMap();
            CreateMap<UpdateCourseRegisterDto, CourseRegister>().ReverseMap();
            CreateMap<ResultCourseRegisterDto, CourseRegister>().ReverseMap();
        }
    }
}
