using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestApplication.DomainLayer.Entities;

namespace TestApplication.Authentication;

public class JwtProvider(IOptions<JwtOptions> jwtOptions,ILogger <JwtProvider>logger) : IJwtProvider
{
    public JwtOptions _JwtOptions { get; } = jwtOptions.Value;
    public ILogger Logger { get; } = logger;

    public (string token, int ExpiryMinute) GenerateToken(ApplicationUser user)
    {
        Claim[] claims = [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
             new Claim(JwtRegisteredClaimNames.Email, user.Email),
              new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
               new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];
        var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOptions.Key));
        var SigningCredentials = new SigningCredentials(SymmetricSecurityKey,SecurityAlgorithms.HmacSha256);
        var expiredate = DateTime.UtcNow.AddMinutes(_JwtOptions.ExpiryMinutes);

        var token = new JwtSecurityToken(
            issuer: _JwtOptions.Issuer,
            audience: _JwtOptions.Audiance,
            signingCredentials: SigningCredentials,
            claims:claims,
            expires: expiredate

            );
        Logger.LogInformation("expiryMinute:{_JwtOptions.ExpiryMinutes},",_JwtOptions.ExpiryMinutes);
        return (token: new JwtSecurityTokenHandler().WriteToken(token), ExpiryMinute: _JwtOptions.ExpiryMinutes *50);
    }

    public string validateToken(string Token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOptions.Key));

        try
            {
            tokenHandler.ValidateToken(Token, new TokenValidationParameters
            {
                IssuerSigningKey = SymmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer= false,
                ClockSkew =TimeSpan.Zero
            }, out SecurityToken securityToken);

            var JwtToken = (JwtSecurityToken)securityToken;
           return JwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

        }
        catch
            {
            return null;
        }
        }}

