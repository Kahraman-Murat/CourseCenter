using CourseCenter.DTO.DTOs.CourseCategoryDtos;
using CourseCenter.DTO.DTOs.UserDtos;
using CourseCenter.Entity.Entities;
using CourseCenter.Entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.CourseDtos
{
    public class ResultCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CourseCategoryId { get; set; }
        public ResultCourseCategoryDto CourseCategory { get; set; }

        public decimal Preis { get; set; }
        public bool IsShown { get; set; }
        public int? AppUserId { get; set; }  //Field for Teacher
        public ResultUserDto AppUser { get; set; } //Object for Teacher
    }
}
