using CourseCenter.Business.Abstract;
using CourseCenter.DataAccess.Abstract;
using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        public BlogManager(IRepository<Blog> _repository, IBlogRepository blogRepository) : base(_repository)
        {
            _blogRepository = blogRepository;
        }

        public void TSetBlogDisplayStatus(int id)
        {
            _blogRepository.SetBlogDisplayStatus(id);
        }

        public List<Blog> TGetBlogsWithCategoryUndWriter(int id = 0)
        {
            return _blogRepository.GetBlogsWithCategoryUndWriter(id);
        }

        public List<Blog> TGetLast4BlogsWithCategories()
        {
            return _blogRepository.GetLast4BlogsWithCategories();
        }

        public List<Blog> TGetBlogsWithCategoryUndWriterByCategoryId(int id = 0)
        {
            return _blogRepository.GetBlogsWithCategoryUndWriterByCategoryId(id);
        }
    }
}
