using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.AuthDtos
{
    public class RequestLoginDto
    {
        [DefaultValue("example@gmail.com")]
        public string Email { get; set; }
        [DefaultValue("1234")]
        public string Password { get; set; }
    }
}
