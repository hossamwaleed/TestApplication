using Microsoft.AspNetCore.Identity;

namespace TestApplication.DomainLayer.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }=string.Empty;
    public string LastName { get; set; } = string.Empty;

    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<FavouriteItem> favouriteItems { get; set; }

    public ICollection<Order> orders { get; set; }
}
