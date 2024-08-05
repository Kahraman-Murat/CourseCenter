using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.BannerDtos;
using CourseCenter.DTO.DTOs.CourseDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(IGenericService<Course> _courseService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _courseService.TGetList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _courseService.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _courseService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateCourseDto createCourseDto)
        {
            var newData = _mapper.Map<Course>(createCourseDto);
            _courseService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateCourseDto updateCourseDto)
        {
            var newData = _mapper.Map<Course>(updateCourseDto);
            _courseService.TUpdate(newData);
            return Ok();
        }
    }
}
