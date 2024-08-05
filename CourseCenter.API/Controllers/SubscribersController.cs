using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.SocialMediaDtos;
using CourseCenter.DTO.DTOs.SubscriberDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController(IGenericService<Subscriber> _subscriberService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _subscriberService.TGetList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _subscriberService.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _subscriberService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateSubscriberDto createSubscriberDto)
        {
            var newData = _mapper.Map<Subscriber>(createSubscriberDto);
            _subscriberService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateSubscriberDto updateSubscriberDto)
        {
            var newData = _mapper.Map<Subscriber>(updateSubscriberDto);
            _subscriberService.TUpdate(newData);
            return Ok();
        }
    }
}
