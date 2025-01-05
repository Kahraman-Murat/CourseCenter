using AutoMapper;
using CourseCenter.DTO.DTOs.TeacherSocialMediaDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class TeacherSocialMediaMapping : Profile
    {
        public TeacherSocialMediaMapping()
        {
            CreateMap<CreateTeacherSocialMediaDto, TeacherSocialMedia>().ReverseMap();
            CreateMap<UpdateTeacherSocialMediaDto, TeacherSocialMedia>().ReverseMap();
            CreateMap<ResultTeacherSocialMediaDto, TeacherSocialMedia>().ReverseMap();
        }
    }
}