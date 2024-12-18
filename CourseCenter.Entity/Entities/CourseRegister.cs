using CourseCenter.Entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Entity.Entities
{
    public class CourseRegister
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public AppUser Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
