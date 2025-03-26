using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Entity.Entities
{
    public class CourseVideo
    {
        public int Id { get; set; }
        public int VideoNumber { get; set; }
        public string VideoTitle { get; set; }
        public string VideoContent { get; set; }
        public string VideoUrl { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
