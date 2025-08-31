namespace TestApplication.DomainLayer.Entities;

public class FavouriteItem
{
    public string UserId { get; set; }
    public int ProductId { get; set; }

    public ApplicationUser User { get; set; }

    public Product Product { get; set; }

    public DateTime CreatedAt { get; set; }= DateTime.Now;
}
