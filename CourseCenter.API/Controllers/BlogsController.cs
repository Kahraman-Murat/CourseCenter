using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.AboutDtos;
using CourseCenter.DTO.DTOs.BlogDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController(IGenericService<Blog> _genericService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _genericService.TGetList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _genericService.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _genericService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateBlogDto createBlogDto)
        {
            var newData = _mapper.Map<Blog>(createBlogDto);
            _genericService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateBlogDto updateBlogDto)
        {
            var newData = _mapper.Map<Blog>(updateBlogDto);
            _genericService.TUpdate(newData);
            return Ok();
        }
    }
}
