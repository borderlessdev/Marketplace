using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Marketplace.Application.Services.Token;

public class TokenController
{
    private const string EmailAlias = "eml";
    private readonly double _tokenLifeTimeMinutes;
    private readonly string _secretKey;

    public TokenController(double tokenLifeTimeMinutes, string secretKey)
    {
        _tokenLifeTimeMinutes = tokenLifeTimeMinutes;
        _secretKey = secretKey;
    }

    public string GenerateToken(string userEmail)
    {
        var claims = new List<Claim>
        {
            new Claim(EmailAlias, userEmail)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tokenLifeTimeMinutes),
            SigningCredentials = new SigningCredentials(SymmetricKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SymmetricKey()
    {
        var symmetricKey = Convert.FromBase64String(_secretKey);

        return new SymmetricSecurityKey(symmetricKey);
    }
}
