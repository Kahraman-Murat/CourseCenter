using CourseCenter.Business.Abstract;
using CourseCenter.Business.Concrete;
using CourseCenter.Business.Helpers;
using CourseCenter.DataAccess.Context;
using CourseCenter.Entity.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
                .AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;

                })
                .AddRoleManager<RoleManager<AppRole>>()
                .AddEntityFrameworkStores<CourseCenterContext>()
                .AddDefaultTokenProviders();

            services
                .AddScoped(typeof(IGenericService<>), typeof(GenericManager<>))

                .AddScoped<IBlogService, BlogManager>()
                .AddScoped<ICourseCategoryService, CourseCategoryManager>()
                .AddScoped<ICourseService, CourseManager>()
                .AddScoped<ISubscriberService, SubscriberManager>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<RolesScannerInAssemblyService>()

                .Configure<TokenSettings>(configuration.GetSection("JWT"))
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IAuthService, AuthService>()

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
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });

        }
    }
}
