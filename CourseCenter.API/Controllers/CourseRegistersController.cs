using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.CourseRegisterDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{

    [Authorize(Roles = "Admin,Content-Manager,Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseRegistersController(ICourseRegisterService _courseRegisterService, IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _courseRegisterService.TGetList();
            var mappedCourses = _mapper.Map<List<ResultCourseRegisterDto>>(datas);
            return Ok(mappedCourses);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _courseRegisterService.TGetById(id);
            var mappedCourse = _mapper.Map<ResultCourseRegisterDto>(course);
            return Ok(mappedCourse);
        }

        [AllowAnonymous]
        [HttpGet("GetCourseByStudentId")]
        public IActionResult GetCourseByStudentId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));
            string claimValue = claim == null ? "0" : claim.Value;

            //var courses = _courseRegisterService.TGetFilteredList(x => x.StudentId.ToString() == "10");//claimValue
            var courses = _courseRegisterService.TGetRegistersWithCourseUndCategory(x => x.StudentId.ToString() == claimValue);
            var mappedCourses = _mapper.Map<List<ResultCourseRegisterDto>>(courses);
            return Ok(mappedCourses);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _courseRegisterService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateCourseRegisterDto createCourseRegisterDto)
        {
            if (createCourseRegisterDto.StudentId == 0)
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));
                string claimValue = claim == null ? "0" : claim.Value;
                createCourseRegisterDto.StudentId = Int32.Parse(claimValue);
            }
            var newData = _mapper.Map<CourseRegister>(createCourseRegisterDto);
            _courseRegisterService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateCourseRegisterDto updateCourseRegisterDto)
        {
            var newData = _mapper.Map<CourseRegister>(updateCourseRegisterDto);
            _courseRegisterService.TUpdate(newData);
            return Ok();
        }

    }
}
