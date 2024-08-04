using AutoMapper;
using CourseCenter.DTO.DTOs.BlogDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class BlogMapping : Profile
    {
        public BlogMapping() 
        {
            CreateMap<CreateBlogDto,Blog>().ReverseMap();
            CreateMap<UpdateBlogDto,Blog>().ReverseMap();
            CreateMap<ResultBlogDto,Blog>().ReverseMap();
        }
    }
}
