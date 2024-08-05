using AutoMapper;
using CourseCenter.DTO.DTOs.MessageDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class MessageMapping : Profile
    {
        public MessageMapping()
        {
            CreateMap<CreateMessageDto, Message>().ReverseMap();
            CreateMap<UpdateMessageDto, Message>().ReverseMap();
            CreateMap<ResultMessageDto, Message>().ReverseMap();
        }
    }
}
