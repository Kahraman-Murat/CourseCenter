using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.BlogCategoryDtos;
using CourseCenter.DTO.DTOs.BlogDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoriesController(IGenericService<BlogCategory> _blogCategoryService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _blogCategoryService.TGetList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _blogCategoryService.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _blogCategoryService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateBlogCategoryDto createBlogCategoryDto)
        {
            var newData = _mapper.Map<BlogCategory>(createBlogCategoryDto);
            _blogCategoryService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            var newData = _mapper.Map<BlogCategory>(updateBlogCategoryDto);
            _blogCategoryService.TUpdate(newData);
            return Ok();
        }
    }
}
