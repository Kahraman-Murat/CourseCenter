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
    public class SubscriberManager : GenericManager<Subscriber>, ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;
        public SubscriberManager(IRepository<Subscriber> _repository, ISubscriberRepository subscriberRepository) : base(_repository)
        {
            _subscriberRepository = subscriberRepository;
        }

        public void TSetSubscriberStatus(int id)
        {
            _subscriberRepository.SetSubscriberStatus(id);
        }
    }
}
