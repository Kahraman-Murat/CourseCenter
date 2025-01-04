using CourseCenter.Entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.TeacherSocialMediaDtos
{
    public class CreateTeacherSocialMediaDto
    {
        public string Icon { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int TeacherId { get; set; }
    }
}
