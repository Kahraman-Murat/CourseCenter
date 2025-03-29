using AutoMapper;
using CourseCenter.DTO.DTOs.CourseVideoDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class CourseVideoMapping : Profile
    {
        public CourseVideoMapping()
        {
            CreateMap<CreateCourseVideoDto, CourseVideo>().ReverseMap();
            CreateMap<UpdateCourseVideoDto, CourseVideo>().ReverseMap();
            CreateMap<ResultCourseVideoDto, CourseVideo>().ReverseMap();
        }
    }
}
