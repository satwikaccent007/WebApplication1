using DataLayer.Irepo;
using DataLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace WebApi
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserModel user)
        {
            var key = _configuration["Jwt:Key"]
                      ?? throw new InvalidOperationException("JWT key not configured.");

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var expireMinutes =
                int.Parse(_configuration["Jwt:ExpireMinutes"] ?? "60");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var securityKey = new SymmetricSecurityKey(   //convert yout key to bytes
                Encoding.UTF8.GetBytes(key));

            var config = new SigningCredentials(  //this tells which key and which algo to use 
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(   //here token is created first header and payload converted to byted them hashed usgin algo and key
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: config
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
