using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.WebUI.DTOs.UserDtos
{
    public class ResultUserRolesDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public List<RoleStateDto> RoleStateDtos { get; set; } = new List<RoleStateDto>();
    }

    
}
