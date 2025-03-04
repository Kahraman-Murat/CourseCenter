﻿using CourseCenter.WebUI.DTOs.BlogCategoryDtos;
using CourseCenter.WebUI.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.WebUI.DTOs.BlogDtos
{
    public class ResultBlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }


        public bool IsShown { get; set; }
        public int BlogCategoryId { get; set; }
        public ResultBlogCategoryDto BlogCategory { get; set; }
        public int BlogWriterId { get; set; }
        public ResultUserSocialMediasDto BlogWriter { get; set; }
    }
}
