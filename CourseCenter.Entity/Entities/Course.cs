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
        public int CategoryId { get; set; }
        public CourseCategory CourseCategory { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preis { get; set; }
        public bool IsShown { get; set; }
    }
}
