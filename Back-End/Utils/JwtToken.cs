using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Back_End.Utils
{
    public class JwtToken
    {
        private readonly IConfiguration _configuration;
        public JwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string userId/*, params string[] roles*/)
        {
            var claims = new List<Claim>
            {
                  new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   new(ClaimTypes.NameIdentifier,userId),
                   new(ClaimTypes.Role,"Admin")
            };


            //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
