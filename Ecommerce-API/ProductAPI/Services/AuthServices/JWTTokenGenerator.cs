using Microsoft.IdentityModel.Tokens;
using ProductAPI.Model.Domain;
using ProductAPI.Model.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductAPI.Services.AuthServices
{
    public class JWTTokenGenerator
    {
        public string CreateJWT(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("versecretkey123456789");
            var identity = new ClaimsIdentity(new Claim[]
            { 
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                new Claim(ClaimTypes.Role , user.Role),
                new Claim(ClaimTypes.Name, user.Name)
            });

            var credential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credential
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
