using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.RoleDtos
{
    public class InfoControllerDto
    {
        public string ControllerName { get; set; }
        public List<InfoActionDto> Actions { get; set; }
    }
}
