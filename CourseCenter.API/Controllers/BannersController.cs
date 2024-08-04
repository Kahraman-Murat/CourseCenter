using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.BannerDtos;
using CourseCenter.DTO.DTOs.BlogDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController(IGenericService<Banner> _bannerService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _bannerService.TGetList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _bannerService.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bannerService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateBannerDto createBannerDto)
        {
            var newData = _mapper.Map<Banner>(createBannerDto);
            _bannerService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateBannerDto updateBannerDto)
        {
            var newData = _mapper.Map<Banner>(updateBannerDto);
            _bannerService.TUpdate(newData);
            return Ok();
        }
    }
}
