using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.WebUI.DTOs.TeacherSocialMediaDtos
{
    public class UpdateTeacherSocialMediaDto
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int TeacherId { get; set; }
    }
}
