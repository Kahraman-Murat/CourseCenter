﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.AuthDtos
{
    public class RequestTokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }
}
