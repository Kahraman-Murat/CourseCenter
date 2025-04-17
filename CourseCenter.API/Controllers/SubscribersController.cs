using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.SocialMediaDtos;
using CourseCenter.DTO.DTOs.SubscriberDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Authorize(Roles = "Admin,Content-Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController(ISubscriberService _subscriberService, IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _subscriberService.TGetList();
            return Ok(datas);
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(CreateSubscriberDto createSubscriberDto)
        {
            var datas = _subscriberService.TGetFilteredList(x => x.Email == createSubscriberDto.Email);
            if (datas.Any())
            {
                return StatusCode(StatusCodes.Status200OK, new { Success = false, Message = "Bu Email Adresi zaten kaydedilmiş !" });
            }
            else if (!datas.Any())
            {
                var newData = _mapper.Map<Subscriber>(createSubscriberDto);
                _subscriberService.TCreate(newData);
                return StatusCode(StatusCodes.Status200OK, new { Success = true, Message = "Email Adresi başarili şekilde kaydedildi." });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new { Success = false, Message = "Kayit işleminde Hata !" });
        }

        [HttpPut]
        public IActionResult Update(UpdateSubscriberDto updateSubscriberDto)
        {
            var newData = _mapper.Map<Subscriber>(updateSubscriberDto);
            _subscriberService.TUpdate(newData);
            return Ok();
        }

        [HttpGet("SetSubscriberStatus/{id}")]
        public IActionResult SetSubscriberStatus(int id)
        {
            _subscriberService.TSetSubscriberStatus(id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("GetSubscriberCount")]
        public IActionResult GetSubscriberCount()
        {
            var courseCount = _subscriberService.TCount();
            return Ok(courseCount);
        }
    }
}
