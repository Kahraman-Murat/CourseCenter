using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.UserDtos
{
    public class AssignRolesToUserDto
    {
        public int UserId { get; set; }
        public List<RolesForUserDto> RolesForUserDtos { get; set; } = new List<RolesForUserDto>();
    }

}
