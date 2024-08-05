using AutoMapper;
using CourseCenter.DTO.DTOs.CourseDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseDto, Course>().ReverseMap();
            CreateMap<UpdateCourseDto, Course>().ReverseMap();
            CreateMap<ResultCourseDto, Course>().ReverseMap();
        }
    }
}

