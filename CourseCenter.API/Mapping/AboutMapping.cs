using AutoMapper;
using CourseCenter.DTO.DTOs.AboutDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class AboutMapping : Profile
    {
        public AboutMapping() 
        {
            CreateMap<CreateAboutDto, About>().ReverseMap();
            CreateMap<UpdateAboutDto, About>().ReverseMap();
        }
    }
}
