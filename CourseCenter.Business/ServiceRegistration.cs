using CourseCenter.Business.Abstract;
using CourseCenter.Business.Concrete;
using CourseCenter.DataAccess.Context;
using CourseCenter.Entity.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped(typeof(IGenericService<>), typeof(GenericManager<>))

                .AddScoped<IBlogService, BlogManager>()
                .AddScoped<ICourseCategoryService, CourseCategoryManager>()
                .AddScoped<ICourseService, CourseManager>()
                .AddScoped<ISubscriberService, SubscriberManager>()
                .AddScoped<IUserService, UserService>()
                
                .Configure<TokenSettings>(configuration.GetSection("JWT"))
                .AddScoped<ITokenService, TokenService>()
                
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                        ValidateLifetime = false,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });

        }
    }
}
