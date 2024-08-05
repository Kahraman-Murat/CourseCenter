using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.ContactDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController(IGenericService<Contact> _contactServıce, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _contactServıce.TGetList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _contactServıce.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _contactServıce.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateContactDto createContactDto)
        {
            var newData = _mapper.Map<Contact>(createContactDto);
            _contactServıce.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateContactDto updateContactDto)
        {
            var newData = _mapper.Map<Contact>(updateContactDto);
            _contactServıce.TUpdate(newData);
            return Ok();
        }
    }
}
