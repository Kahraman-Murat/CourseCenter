using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        List<Blog> TGetBlogsWithCategoryUndWriter(int id = 0);
        List<Blog> TGetBlogsWithCategoryUndWriterByCategoryId(int id = 0);
        List<Blog> TGetLast4BlogsWithCategories();

        void TSetBlogDisplayStatus(int id);
    }
}
