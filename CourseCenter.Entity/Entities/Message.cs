using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Entity.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime EmailDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

    }
}
