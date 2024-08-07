using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.WebUI.DTOs.SubscriberDtos
{
    public class ResultSubscriberDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
