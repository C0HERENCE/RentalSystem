using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using RentalSystem.Models;

namespace RentalSystem.Services
{
    public class JwtService
    {
        private readonly JwtConfig _jwtConfig;
        
        private readonly string _identifier = "userid";

        public JwtService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }
        
        public string GenerateToken(int userId)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(_identifier, userId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Secret)), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int GetUserId(HttpContext httpContext)
        {
            try
            {
                var claim = httpContext.User.Claims.First(x => x.Type == _identifier);
                return int.Parse(claim.Value);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}