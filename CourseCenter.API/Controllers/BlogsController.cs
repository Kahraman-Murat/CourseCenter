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
    public class BlogsController(IBlogService _blogService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _blogService.TGetBlogsWithCategories();
            var blogs = _mapper.Map<List<ResultBlogDto>>(datas);
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _blogService.TGetById(id);
            var blog = _mapper.Map<ResultBlogDto>(data);
            return Ok(blog);
        }

        [HttpGet("GetLast4Blogs")]
        public IActionResult GetLast4Blogs()
        {
            var datas = _blogService.TGetLast4BlogsWithCategories();
            var blogs= _mapper.Map<List<ResultBlogDto>>(datas); 
            return Ok(blogs);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _blogService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateBlogDto createBlogDto)
        {
            var newData = _mapper.Map<Blog>(createBlogDto);
            _blogService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateBlogDto updateBlogDto)
        {
            var newData = _mapper.Map<Blog>(updateBlogDto);
            _blogService.TUpdate(newData);
            return Ok();
        }

        [HttpGet("SetBlogDisplayStatus/{id}")]
        public IActionResult SetBlogDisplayStatus(int id)
        {
            _blogService.TSetBlogDisplayStatus(id);
            return Ok();
        }
    }
}
