using CourseCenter.Business.Abstract;
using CourseCenter.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _tokenSettings;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IOptions<TokenSettings> options, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _tokenSettings = options.Value;
        }

        public async Task<JwtSecurityToken> CreateAccessToken(AppUser user, IList<string> roles)
        {
            // Claims e Jwt_Id , User_Id , User_Email eklenmesi
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            // Claims e User_Roles eklenmesi
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // key olusturma
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));

            // token olusturma
            var token = new JwtSecurityToken(
                audience: _tokenSettings.Audience,
                issuer: _tokenSettings.Issuer,
                expires: DateTime.Now.AddMinutes(_tokenSettings.TokenValidityInMunites),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            //Claims lerin AspNetUserClaims Tablosuna eklenmesi
            await _userManager.AddClaimsAsync(user, claims);

            return token;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret)),
                ValidateLifetime = false,
            };

            JwtSecurityTokenHandler tokenHandler = new();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token Bulunamadi");

            return principal;

        }
    }
}
