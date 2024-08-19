using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.ContactDtos;
using CourseCenter.DTO.DTOs.CourseCategoryDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoriesController(ICourseCategoryService _courseCategoryService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _courseCategoryService.TGetList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _courseCategoryService.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _courseCategoryService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateCourseCategoryDto createCourseCategoryDto)
        {
            var newData = _mapper.Map<CourseCategory>(createCourseCategoryDto);
            _courseCategoryService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateCourseCategoryDto updateCourseCategoryDto)
        {
            var newData = _mapper.Map<CourseCategory>(updateCourseCategoryDto);
            _courseCategoryService.TUpdate(newData);
            return Ok();
        }

        [HttpGet("SetCourseCategoryDisplayStatus/{id}")]
        public IActionResult SetCourseCategoryDisplayStatus(int id)
        {
            _courseCategoryService.TSetCourseCategoryDisplayStatus(id);
            return Ok();
        }

        [HttpGet("GetActiveCategories")]
        public IActionResult GetActiveCategories()
        {
            var datas = _courseCategoryService.TGetFilteredList(x => x.IsShown == true).ToList();
            return Ok(datas);
        }
    }
}
