using AutoMapper;
using CourseCenter.DTO.DTOs.BlogCategoryDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class BlogCategoryMapping : Profile
    {
        public BlogCategoryMapping()
        {
            CreateMap<CreateBlogCategoryDto, BlogCategory>().ReverseMap();
            CreateMap<UpdateBlogCategoryDto, BlogCategory>().ReverseMap();
            CreateMap<ResultBlogCategoryDto, BlogCategory>().ReverseMap();
        }
    }
}
