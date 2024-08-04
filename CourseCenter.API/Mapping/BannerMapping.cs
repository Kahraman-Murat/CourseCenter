using AutoMapper;
using CourseCenter.DTO.DTOs.BannerDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class BannerMapping : Profile
    {
        public BannerMapping()
        {
            CreateMap<CreateBannerDto, Banner>().ReverseMap();
            CreateMap<UpdateBannerDto, Banner>().ReverseMap();
        }
    }
}
