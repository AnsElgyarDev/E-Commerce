using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceApp.Services;

public class TokenServices : ITokenServices
{
    private readonly IConfiguration _config;
    private readonly UserManager<AppliactionUser> _userManager;

    public TokenServices(IConfiguration config, UserManager<AppliactionUser> userManager)
    {
        this._config = config;
        this._userManager = userManager;
    }

    public async Task<string> CreateToken(AppliactionUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.FullName)
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

        var tokenDesciptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow,
            SigningCredentials = creds,
            Issuer = _config["Jwt:Issuer"],  
            Audience = _config["Jwt:Audience"]  
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDesciptor);

        return  tokenHandler.WriteToken(token);    
        
    }
}