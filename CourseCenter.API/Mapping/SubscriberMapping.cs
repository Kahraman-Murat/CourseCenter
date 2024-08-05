using AutoMapper;
using CourseCenter.DTO.DTOs.SubscriberDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class SubscriberMapping : Profile
    {
        public SubscriberMapping()
        {
            CreateMap<CreateSubscriberDto, Subscriber>().ReverseMap();
            CreateMap<UpdateSubscriberDto, Subscriber>().ReverseMap();
            CreateMap<ResultSubscriberDto, Subscriber>().ReverseMap();
        }
    }
}