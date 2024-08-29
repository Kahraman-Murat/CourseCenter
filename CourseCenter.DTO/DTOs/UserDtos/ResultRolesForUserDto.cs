using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.UserDtos
{
    public class ResultRolesForUserDto
    {
        public bool UserExists { get; set; } = false;
        public List<RolesForUserDto> RolesForUserDtos { get; set; } = new List<RolesForUserDto>();

    }

    
}
