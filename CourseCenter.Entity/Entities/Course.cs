using System;
using System.Collections.Generic;
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
        public int CategoryId { get; set; }
        public CourseCategory CourseCategory { get; set; }
        public decimal Preis { get; set; }
        public bool IsShown { get; set; }
    }
}
