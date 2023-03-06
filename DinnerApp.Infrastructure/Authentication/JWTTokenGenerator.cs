using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using DinnerApp.Application.Common.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;

namespace DinnerApp.Infrastructure.Authentication
{
    public class JWTTokenGenerator : IJWTTokenGenerator
    {
        private readonly JWTSettings _jwtSettings;
        public JWTTokenGenerator(IOptions<JWTSettings> jwtOptions){
            _jwtSettings = jwtOptions.Value;
        }
        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256
            );
            
            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                
            };

            var SecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                expires: DateTime.Now.AddDays(_jwtSettings.ExpiryMinutes),
                audience : _jwtSettings.Audiance,
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(SecurityToken);
        }
    }
}