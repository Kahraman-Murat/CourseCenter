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
        public int AppUserId { get; set; }   //Field for Student
        public AppUser AppUser { get; set; } //Object for Student
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
