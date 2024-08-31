using Microsoft.AspNetCore.Identity;
using CourseCenter.DTO.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseCenter.Business.Abstract
{
    public interface IAuthService
    {
        Task<(bool Success, string[] Errors)> LoginAsync(UserLoginDto userLoginDto);
        Task<bool> LogoutAsync();        

    }
}
