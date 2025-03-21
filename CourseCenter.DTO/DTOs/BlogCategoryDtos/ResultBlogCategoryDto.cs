﻿using CourseCenter.DTO.DTOs.BlogDtos;
using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DTO.DTOs.BlogCategoryDtos
{
    public class ResultBlogCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ResultBlogDto> Blogs { get; set; }
    }
}
