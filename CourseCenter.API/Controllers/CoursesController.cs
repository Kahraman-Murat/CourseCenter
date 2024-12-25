using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.BannerDtos;
using CourseCenter.DTO.DTOs.CourseDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Authorize(Roles = "Admin,Content-Manager,Teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(ICourseService _courseService, IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _courseService.TGetCoursesWithCategoryUndTeacher();
            var mappedCourses = _mapper.Map<List<ResultCourseDto>>(datas);
            return Ok(mappedCourses);
        }

        [AllowAnonymous]
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


        [HttpGet("SetCourseDisplayStatus/{id}")]
        public IActionResult SetCourseDisplayStatus(int id)
        {
            _courseService.TSetCourseDisplayStatus(id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("GetActiveCourses")]
        public IActionResult GetActiveCourses()
        {
            var datas = _courseService.TGetFilteredList(x => x.IsShown == true).ToList();
            return Ok(datas);
        }

        [AllowAnonymous]
        [HttpGet("GetCourseCount")]
        public IActionResult GetCourseCount()
        {
            var courseCount = _courseService.TCount();
            return Ok(courseCount);
        }

        [AllowAnonymous]
        [HttpGet("GetCourseByTeacherId/{id}")]
        public IActionResult GetCourseByTeacherId(string id)
        {
            var courses = _courseService.TGetCoursesWithCategoryUndTeacher().Where(x => x.TeacherId.ToString() == id).ToList();
            var mappedCourses = _mapper.Map<List<ResultCourseDto>>(courses);

            return Ok(mappedCourses);
        }
    }
}
