using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.BlogDtos;
using CourseCenter.DTO.DTOs.UserDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Authorize(Roles = "Admin,Content-Manager,Editor")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController(IBlogService _blogService, IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _blogService.TGetBlogsWithCategoryUndWriter();
            var blogs = _mapper.Map<List<ResultBlogDto>>(datas);
            return Ok(blogs);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _blogService.TGetBlogsWithCategoryUndWriter(id);            
            var blog = _mapper.Map<ResultBlogDto>(data.FirstOrDefault());
            return Ok(blog);
        }

        [AllowAnonymous]
        [HttpGet("GetByCategoryId/{id}")]
        public IActionResult GetByCategoryId(int id)
        {
            var datas = _blogService.TGetBlogsWithCategoryUndWriterByCategoryId(id);
            var blog = _mapper.Map<List<ResultBlogDto>>(datas);
            return Ok(blog);
        }

        [AllowAnonymous]
        [HttpGet("GetLast4Blogs")]
        public IActionResult GetLast4Blogs()
        {
            var datas = _blogService.TGetLast4BlogsWithCategories();
            var blogs = _mapper.Map<List<ResultBlogDto>>(datas);
            return Ok(blogs);
        }

        [AllowAnonymous]
        [HttpGet("GetBlogsByWriterId/{id}")]
        public IActionResult GetBlogsByWriterId(string id)
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));
            string claimValue = claim == null ? "0" : claim.Value;
            
            var courses = _blogService.TGetBlogsWithCategoryUndWriter().Where(x => x.BlogWriterId.ToString() == claimValue).ToList();
            var mappedBlogs = _mapper.Map<List<ResultBlogDto>>(courses);

            return Ok(mappedBlogs);
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
            if (createBlogDto.BlogWriterId == 0)
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));
                string claimValue = claim == null ? "0" : claim.Value;
                createBlogDto.BlogWriterId = Int32.Parse(claimValue);
            }

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

        [AllowAnonymous]
        [HttpGet("GetBlogCount")]
        public IActionResult GetBlogCount()
        {
            var blogCount = _blogService.TCount();
            return Ok(blogCount);
        }
    }
}
