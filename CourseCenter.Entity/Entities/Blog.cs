using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Entity.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }


        public bool IsShown { get; set; }
        public int BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
