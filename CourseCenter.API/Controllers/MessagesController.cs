using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.BannerDtos;
using CourseCenter.DTO.DTOs.MessageDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(IGenericService<Message> _messageService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _messageService.TGetList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _messageService.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _messageService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateMessageDto createMessageDto)
        {
            var newData = _mapper.Map<Message>(createMessageDto);
            _messageService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateMessageDto updateMessageDto)
        {
            var newData = _mapper.Map<Message>(updateMessageDto);
            _messageService.TUpdate(newData);
            return Ok();
        }
    }
}
