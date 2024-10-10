using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.RoleDtos
{
    public class InfoActionDto
    {
        public string ActionName { get; set; }
        public List<string> AuthorizeRoles { get; set; }
    }
}
