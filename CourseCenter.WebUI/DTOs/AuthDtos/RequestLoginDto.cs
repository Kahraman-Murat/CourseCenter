﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.WebUI.DTOs.AuthDtos
{
    public class RequestLoginDto
    {
        public string Email { get; set; }        
        public string Password { get; set; }
    }
}
