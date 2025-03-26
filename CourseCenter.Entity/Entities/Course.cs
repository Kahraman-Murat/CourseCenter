using CourseCenter.Entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Entity.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CourseCategoryId { get; set; }
        public CourseCategory CourseCategory { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preis { get; set; }
        public bool IsShown { get; set; }

        public int TeacherId { get; set; }
        public AppUser Teacher { get; set; }
        public List<CourseRegister> CourseRegisters { get; set; }
        public List<CourseVideo> CourseVideos { get; set; }
    }
}
