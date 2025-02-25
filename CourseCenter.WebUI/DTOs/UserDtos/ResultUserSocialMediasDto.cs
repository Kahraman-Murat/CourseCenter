using CourseCenter.WebUI.DTOs.TeacherSocialMediaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.WebUI.DTOs.UserDtos
{
    public class ResultUserSocialMediasDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ImageUrl { get; set; }
        public List<ResultTeacherSocialMediaDto> TeacherSocialMedias { get; set; }

    }
}
