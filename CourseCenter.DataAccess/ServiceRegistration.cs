using CourseCenter.DataAccess.Abstract;
using CourseCenter.DataAccess.Concrete;
using CourseCenter.DataAccess.Context;
using CourseCenter.DataAccess.Repositories;
using CourseCenter.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DataAccess
{
    public static class ServiceRegistration
    {
        public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services
                .AddDbContext<CourseCenterContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
                })

                .AddScoped<IBlogCategoryRepository, BlogCategoryRepository>()
                .AddScoped<IBlogRepository, BlogRepository>()
                .AddScoped<ICourseCategoryRepository, CourseCategoryRepository>()
                .AddScoped<ICourseRegisterRepository, CourseRegisterRepository>()
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<ICourseVideoRepository, CourseVideoRepository>()
                .AddScoped(typeof(IRepository<>), typeof(GenericRepository<>))
                .AddScoped<ISubscriberRepository, SubscriberRepository>()
                


                //.AddIdentityCore<AppUser>(opt =>
                //{
                //    opt.Password.RequireNonAlphanumeric = false;
                //    opt.Password.RequiredLength = 2;
                //    opt.Password.RequireLowercase = false;
                //    opt.Password.RequireUppercase = false;
                //    opt.Password.RequireDigit = false;
                //    opt.SignIn.RequireConfirmedEmail = false;
                //})
                //.AddRoles<AppRole>()
                //.AddEntityFrameworkStores<CourseCenterContext>()
                ;

        }
    }
}
