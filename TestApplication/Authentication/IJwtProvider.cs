using TestApplication.ApplicationLayer.abstractions;
using TestApplication.DomainLayer.Entities;

namespace TestApplication.Authentication;

public interface IJwtProvider
{
    (string token, int ExpiryMinute) GenerateToken(ApplicationUser user);
    public string validateToken(string Token);
}
