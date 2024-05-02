using Domain.Entidades.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infraestructura.ConfiguracionApp
{
    public class AutenticacionService
    {
        public static string CrearToken(Admin user, IConfiguration configuration)
        {
            var jwt = configuration.GetSection("Jwt").Get<JwtConfig>();
            
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("IdAdmin",user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                  jwt.Issuer,
                  jwt.Audience,
                  claims,
                  expires: DateTime.Now.AddMinutes(5),
                  signingCredentials: singIn
                );

            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }
    }
}
