using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.BannerDtos;
using CourseCenter.DTO.DTOs.SocialMediaDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController(IGenericService<SocialMedia> _socialMediaService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _socialMediaService.TGetList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _socialMediaService.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _socialMediaService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateSocialMediaDto createSocialMediaDto)
        {
            var newData = _mapper.Map<SocialMedia>(createSocialMediaDto);
            _socialMediaService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var newData = _mapper.Map<SocialMedia>(updateSocialMediaDto);
            _socialMediaService.TUpdate(newData);
            return Ok();
        }
    }
}
