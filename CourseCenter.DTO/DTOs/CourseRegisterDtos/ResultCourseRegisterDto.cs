using CourseCenter.Entity.Entities.Identity;
using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseCenter.DTO.DTOs.UserDtos;
using CourseCenter.DTO.DTOs.CourseDtos;

namespace CourseCenter.DTO.DTOs.CourseRegisterDtos
{
    public class ResultCourseRegisterDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public ResultUserDto Student { get; set; }
        public int CourseId { get; set; }
        public ResultCourseDto Course { get; set; }
    }
}
