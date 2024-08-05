using AutoMapper;
using CourseCenter.DTO.DTOs.TestimonialDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class TestimonialMapping : Profile
    {
        public TestimonialMapping()
        {
            CreateMap<CreateTestimonialDto, Testimonial>().ReverseMap();
            CreateMap<UpdateTestimonialDto, Testimonial>().ReverseMap();
            CreateMap<ResultTestimonialDto, Testimonial>().ReverseMap();
        }
    }
}