using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.TeacherSocialMediaDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Authorize(Roles = "Admin,Content-Manager,Teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSocialMediasController(IGenericService<TeacherSocialMedia> _teacherSocialMediaService, IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _teacherSocialMediaService.TGetList();
            return Ok(datas);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _teacherSocialMediaService.TGetById(id);
            return Ok(data);
        }

        [HttpGet("GetByTeacherId")]
        public IActionResult GetByTeacherId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));
            string claimValue = claim == null ? "0" : claim.Value;

            var medias = _teacherSocialMediaService.TGetFilteredList(x => x.TeacherId.ToString() == claimValue).ToList();
            var mappedMedias = _mapper.Map<List<ResultTeacherSocialMediaDto>>(medias);

            return Ok(mappedMedias);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _teacherSocialMediaService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateTeacherSocialMediaDto createTeacherSocialMediaDto)
        {
            if (createTeacherSocialMediaDto.TeacherId == 0)
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));
                string claimValue = claim == null ? "0" : claim.Value;
                createTeacherSocialMediaDto.TeacherId = Int32.Parse(claimValue);
            }
            var newData = _mapper.Map<TeacherSocialMedia>(createTeacherSocialMediaDto);
            _teacherSocialMediaService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateTeacherSocialMediaDto updateTeacherSocialMediaDto)
        {
            var newData = _mapper.Map<TeacherSocialMedia>(updateTeacherSocialMediaDto);
            _teacherSocialMediaService.TUpdate(newData);
            return Ok();
        }
    }
}
