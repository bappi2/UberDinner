using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UberDinner.Application.Common.Interfaces.Authentication;
using UberDinner.Application.Services;
using UberDinner.Domain.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace UberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private IDateTimeProvider _dateTimeProvider;
    
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }
    public string GenerateToken(User user)
    {
        var key = new byte[32];
        using (var generator = new RNGCryptoServiceProvider())
        {
            generator.GetBytes(key);
        }

        var signedCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var securityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
            signingCredentials: signedCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}

/*
 A DateTimeProvider is often used in 
 software development to abstract the 
 system's current date and time. 
 This abstraction is particularly useful 
 for testing purposes.  When you write 
 unit tests for your code, you want your
  tests to be deterministic, 
  meaning they should produce the 
  same results every time they run, 
  regardless of when they are run. 
  If your code uses DateTime.Now or 
  DateTime.UtcNow directly, 
  it introduces a non-deterministic 
  element to your code, which can make 
  it harder to test.  By using a 
  DateTimeProvider, you can control 
  the current date and time in your tests. 
  For example, you can create a 
  FakeDateTimeProvider that always 
  returns a specific date and time, 
  and use this in your tests.
*/