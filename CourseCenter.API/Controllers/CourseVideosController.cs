using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.CourseDtos;
using CourseCenter.DTO.DTOs.CourseVideoDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Student,Content-Manager")]
    [Route("api/[controller]")]
    [ApiController]

    public class CourseVideosController(ICourseVideoService _courseVideoService, IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _courseVideoService.TGetList();
            return Ok(datas);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _courseVideoService.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _courseVideoService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateCourseVideoDto createCourseVideoDto)
        {
            var newData = _mapper.Map<CourseVideo>(createCourseVideoDto);
            _courseVideoService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateCourseVideoDto updateCourseVideoDto)
        {
            var newData = _mapper.Map<CourseVideo>(updateCourseVideoDto);
            _courseVideoService.TUpdate(newData);
            return Ok();
        }
                
        [HttpGet("GetVideoListByCourseId/{id}")]
        public IActionResult GetVideoListByCourseId(int id)
        {
            var datas = _courseVideoService.TGetVideosWithCourseByCourseId(id);
            var videos = _mapper.Map<List<ResultCourseVideoDto>>(datas);
            return Ok(videos);
        }

    }
}
