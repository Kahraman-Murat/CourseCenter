using AutoMapper;
using CourseCenter.DTO.DTOs.SocialMediaDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class SocialMediaMapping : Profile
    {
        public SocialMediaMapping()
        {
            CreateMap<CreateSocialMediaDto, SocialMedia>().ReverseMap();
            CreateMap<UpdateSocialMediaDto, SocialMedia>().ReverseMap();
            CreateMap<ResultSocialMediaDto, SocialMedia>().ReverseMap();
        }
    }
}