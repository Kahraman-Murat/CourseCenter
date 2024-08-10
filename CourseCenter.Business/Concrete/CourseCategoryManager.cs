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
    public class CourseCategoryManager : GenericManager<CourseCategory>, ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        public CourseCategoryManager(IRepository<CourseCategory> _repository, ICourseCategoryRepository courseCategoryRepository) : base(_repository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }

        public void TSetCourseCategoryDisplayStatus(int id)
        {
            _courseCategoryRepository.SetCourseCategoryDisplayStatus(id);
        }

    }
}
