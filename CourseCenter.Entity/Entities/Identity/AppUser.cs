﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Entity.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }

        public string? ImageUrl { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public List<Course> Courses { get; set; } 

        public List<CourseRegister> CourseRegisters { get; set; }
    }
}
