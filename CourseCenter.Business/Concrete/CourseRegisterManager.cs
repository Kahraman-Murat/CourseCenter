using CourseCenter.Business.Abstract;
using CourseCenter.DataAccess.Abstract;
using CourseCenter.DataAccess.Concrete;
using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Concrete
{
    public class CourseRegisterManager : GenericManager<CourseRegister>, ICourseRegisterService
    {
        private readonly ICourseRegisterRepository _courseRegisterRepository;

        public CourseRegisterManager(IRepository<CourseRegister> _repository, ICourseRegisterRepository courseRegisterRepository) : base(_repository)
        {
            _courseRegisterRepository = courseRegisterRepository;
        }

        public List<CourseRegister> TGetRegistersWithCourseUndCategory(Expression<Func<CourseRegister, bool>> filter)
        {
            return _courseRegisterRepository.GetRegistersWithCourseUndCategory(filter);
        }
    }
}
