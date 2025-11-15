using Humanizer.Bytes;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WorldModel;

namespace comp584Server
{
    public class JwtHandling(UserManager<WorldModelUsers> userManager,IConfiguration configuration )
    {
        public async Task<JwtSecurityToken> GenerateToken(WorldModelUsers user)
        {
            return new JwtSecurityToken
            (
                issuer: configuration["Issuer"],
                audience: configuration["Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["ExpiryMinutes"])),
                claims : await GetClaimsAsync(user),
                signingCredentials: GetSigningCredentials()
            );
        }
        private SigningCredentials GetSigningCredentials() 
        {
            Byte[] key = Convert.FromBase64String(configuration["JwtSettings:SecretKey"]!);
            SymmetricSecurityKey signingkey = new (key);
            return new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256);
        } 

        private async Task<List<Claim>> GetClaimsAsync(WorldModelUsers user)
        {
            List<Claim> claims = [new Claim(ClaimTypes.Name, user.UserName!)];
            //claims.AddRange((await userManager.GetRolesAsync(user)).Select(role => new Claim(ClaimTypes.Role, role)));
            foreach (var role in await userManager.GetRolesAsync(user)) { 
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
