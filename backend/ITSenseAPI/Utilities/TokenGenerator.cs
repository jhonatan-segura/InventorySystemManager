using FinanzautoAPI.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITSenseAPI.Utilities
{
   public class TokenGenerator(IOptions<JwtSettings> options)
   {
      private readonly JwtSettings _jwtSettings = options.Value;

      public string GenerateToken(string email)
      {
         var claims = new[]
         {
                new Claim(ClaimTypes.Name, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

         var token = new JwtSecurityToken(
             issuer: _jwtSettings.Issuer,
             audience: _jwtSettings.Audience,
             claims: claims,
             expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
             signingCredentials: creds
         );

         return new JwtSecurityTokenHandler().WriteToken(token);
      }
   }
}
