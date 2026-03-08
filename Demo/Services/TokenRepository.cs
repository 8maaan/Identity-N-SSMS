using Demo.Data;
using Demo.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.Services
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(ApplicationUser user)
        {
            
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Email, user.Email!),
                    new(ClaimTypes.Name, user.UserName!),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Iat,
                        DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                        ClaimValueTypes.Integer64)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
