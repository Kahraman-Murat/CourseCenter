using CourseCenter.Entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Entity.Entities
{
    public class TeacherSocialMedia
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int TeacherId { get; set; }
        public AppUser Teacher { get; set; }
    }
}
