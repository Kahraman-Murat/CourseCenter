using AutoMapper;
using CourseCenter.DTO.DTOs.ContactDtos;
using CourseCenter.Entity.Entities;

namespace CourseCenter.API.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<CreateContactDto, Contact>().ReverseMap();
            CreateMap<UpdateContactDto, Contact>().ReverseMap();
            CreateMap<ResultContactDto, Contact>().ReverseMap();
        }
    }
}

