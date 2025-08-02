using Microsoft.EntityFrameworkCore;

namespace TestApplication.DomainLayer.Entities;
[Owned]
public class RefreshToken
{
    public string Token { get; set; } =string.Empty;
    public DateTime ExpiresOn { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? RevokedOn { get; set; }

    public bool isActive => DateTime.UtcNow < ExpiresOn && RevokedOn is null;
    public bool isExpired => DateTime.UtcNow >= ExpiresOn;
}
