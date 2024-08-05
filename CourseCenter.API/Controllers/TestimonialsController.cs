using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.SubscriberDtos;
using CourseCenter.DTO.DTOs.TestimonialDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController(IGenericService<Testimonial> _testimonialService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var datas = _testimonialService.TGetList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _testimonialService.TGetById(id);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _testimonialService.TDelete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateTestimonialDto createTestimonialDto)
        {
            var newData = _mapper.Map<Testimonial>(createTestimonialDto);
            _testimonialService.TCreate(newData);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateTestimonialDto updateTestimonialDto)
        {
            var newData = _mapper.Map<Testimonial>(updateTestimonialDto);
            _testimonialService.TUpdate(newData);
            return Ok();
        }
    }
}
