using Onion.JwtApp.Application.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Onion.JwtApp.API.Tools
{
    public class JwtGenerator
    {
        public static TokenResponseDto GenerateToken(CheckAppUserResponseDto dto)
        {
            List<Claim> claims = new List<Claim>();
            if (!string.IsNullOrEmpty(dto.Role))
                claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            if (!string.IsNullOrEmpty(dto.Username))
                claims.Add(new Claim("Username", dto.Username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()));

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtDefaults.IssuerSigningKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddDays(JwtDefaults.Expire);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: JwtDefaults.ValidIssuer,
                audience: JwtDefaults.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: signingCredentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            //handler.WriteToken()
            return new TokenResponseDto(handler.WriteToken(jwtSecurityToken), expireDate);
        }
    }
}
