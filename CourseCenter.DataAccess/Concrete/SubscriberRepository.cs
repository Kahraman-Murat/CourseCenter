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
    public class SubscriberRepository : GenericRepository<Subscriber>, ISubscriberRepository
    {        
        public SubscriberRepository(CourseCenterContext _context) : base(_context)
        {

        }

        public void SetSubscriberStatus(int id)
        {
            var subscriber = _context.Subscribers.Find(id);
            if (subscriber != null)
            {
                subscriber.IsActive = !subscriber.IsActive;
            }
            _context.SaveChanges();
        }
    }
}
