using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EasyCookAPI.Core.Helpers
{
    public interface IHelper
    {
        string GenerarToken(User user);
        string GetToken();
        int DecodeJwt(string token);
        string EncryptPassSha25(string password);
    }

    public class Helper : IHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Helper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerarToken(User user)
        {
            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Username", user.Username),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Secret_key").Get<string>() ?? string.Empty));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GetToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                throw new UnauthorizedAccessException("No se encontró el token.");
            }
            return token;
        }
        public int DecodeJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if(jsonToken != null)
            {
                foreach (var data in jsonToken.Claims)
                {
                    if (data.Type == "Id")
                    {
                        return int.Parse(data.Value);
                    }
                }
            }

            return 0;
        }
        public string EncryptPassSha25(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            
            return sb.ToString();
        }
    }
}
