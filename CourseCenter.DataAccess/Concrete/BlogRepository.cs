using CourseCenter.DataAccess.Abstract;
using CourseCenter.DataAccess.Context;
using CourseCenter.DataAccess.Repositories;
using CourseCenter.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DataAccess.Concrete
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(CourseCenterContext _context) : base(_context)
        {

        }

        public List<Blog> GetBlogsWithCategoryUndWriter(int id = 0)
        {
            var query = _context.Blogs.AsQueryable();
            if (id > 0)
                query = query.Where(b => b.Id == id);

            return query.Include(x => x.BlogCategory).Include(w => w.BlogWriter).ThenInclude(x=>x.TeacherSocialMedias).ToList();
        }

        public List<Blog> GetBlogsWithCategoryUndWriterByCategoryId(int id = 0)
        {
            var query = _context.Blogs.Include(x => x.BlogCategory).AsQueryable();
            if (id > 0)
                query = query.Where(b => b.BlogCategory.Id == id);

            return query.Include(w => w.BlogWriter).ThenInclude(x => x.TeacherSocialMedias).ToList();
        }

        public List<Blog> GetLast4BlogsWithCategories()
        {
            return _context.Blogs.Include(c => c.BlogCategory).OrderByDescending(o => o.Id).Take(4).ToList();
        }

        public void SetBlogDisplayStatus(int id)
        {
            var blog = _context.Blogs.Find(id);
            if (blog != null)
            {
                blog.IsShown = !blog.IsShown;
            }
            _context.SaveChanges();
        }
    }
}
