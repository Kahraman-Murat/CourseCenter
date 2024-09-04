using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CourseCenter.DTO.DTOs.AuthDtos;

namespace CourseCenter.Business.Abstract
{
    public interface IAuthService
    {        
        Task<ResponseLoginDto> LoginAsync(RequestLoginDto userLoginDto);
        Task<ResponseTokenDto> RefreshTokenAsync(RequestTokenDto requestTokenDto);
        Task<bool> RevokeAsync(RequestRevokeDto requestRevokeDto);
        Task RevokeAllAsync();        

    }
}
