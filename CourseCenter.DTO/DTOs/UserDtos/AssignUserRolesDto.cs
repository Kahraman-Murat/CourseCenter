using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.UserDtos
{
    public class AssignUserRolesDto
    {
        public int UserId { get; set; }
        public List<RoleStateDto> RoleStateDtos { get; set; } = new List<RoleStateDto>();
    }

}
