using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseCenter.WebUI.DTOs.UserDtos;
using CourseCenter.WebUI.DTOs.CourseDtos;

namespace CourseCenter.WebUI.DTOs.CourseRegisterDtos
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
