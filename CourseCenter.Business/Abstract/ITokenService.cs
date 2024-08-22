using CourseCenter.Entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Abstract
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateAccessToken(AppUser user, IList<string> roles);
        string CreateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
