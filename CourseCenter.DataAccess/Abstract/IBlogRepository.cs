using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DataAccess.Abstract
{
    public interface IBlogRepository : IRepository<Blog>
    {
        List<Blog> GetBlogsWithCategoryUndWriter();

        void SetBlogDisplayStatus(int id);

        List<Blog> GetLast4BlogsWithCategories();
    }
}
