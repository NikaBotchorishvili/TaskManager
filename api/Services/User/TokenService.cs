using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Services;

public class TokenService: ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    private readonly int _expiresInDays;
    private readonly int _expiresInHours;
    private readonly int _expiresInMinutes;
    private readonly int _expiresInSeconds;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SigningKey"]));
        _expiresInDays = int.TryParse(config["JWT:ExpiresInDays"], out var days) ? days : 0;
        _expiresInHours = int.TryParse(config["JWT:ExpiresInHours"], out var hours) ? hours : 0;
        _expiresInMinutes = int.TryParse(config["JWT:ExpiresInMinutes"], out var minutes) ? minutes : 0;
        _expiresInSeconds = int.TryParse(config["JWT:ExpiresInSeconds"], out var seconds) ? seconds : 0;
    }
    
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Sub, user.Id),
        };
            Console.WriteLine(user.Id);
        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var now = DateTime.UtcNow;
        var expires = now.Add(new TimeSpan(
            _expiresInDays,
            _expiresInHours,
            _expiresInMinutes,
            _expiresInSeconds
        ));

        // Set NotBefore to a reasonable value (e.g., current time)
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            NotBefore = now,
            SigningCredentials = credentials,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}