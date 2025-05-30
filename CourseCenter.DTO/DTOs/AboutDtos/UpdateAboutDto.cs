﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.AboutDtos
{
    public class UpdateAboutDto
    {
        public int Id { get; set; }
        public string OurMission { get; set; }
        public string OurVision { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
    }
}
