using Drug_Procurement.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Drug_Procurement.Security.Hash;

public interface IJwtAuth
{
    string GenerateToken(Users user);
    string GenerateRefreshToken();
}

public class JwtAuth : IJwtAuth
{
    private readonly IConfiguration _config;

    public JwtAuth(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(Users user)
    {
        /*
    * Username
    * FirstName
    * Email
    * RoleId
    */
        IEnumerable<Claim> claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim("FirstName", user.FirstName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("RoleId", user.RoleId.ToString()),
            new Claim("UserId", user.Id.ToString()),
            new Claim("LastName", user.LastName)
        };
        var jwtSecurityToken = GetToken(claims);
        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;
    }
    private JwtSecurityToken GetToken(IEnumerable<Claim> claims)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
        return new JwtSecurityToken(
            issuer: _config["Jwt:ValidIssuer"],
            audience: _config["Jwt:ValidAudience"],
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:TokenValidityInMinutes"])),
            claims: claims,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
    }
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        };
    }
}
