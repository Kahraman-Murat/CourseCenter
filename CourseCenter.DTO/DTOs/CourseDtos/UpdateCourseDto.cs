using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.CourseDtos
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CourseCategoryId { get; set; }
        public decimal Preis { get; set; }
        public bool IsShown { get; set; }
        public int? AppUserId { get; set; }  //Field for Teacher
    }
}
