namespace TestApplication.DomainLayer.Entities;

public sealed class Shop
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string address { get; set; }
    public ShopLocation Location { get; set; }

    public ICollection<Product> Products { get; set; }
}
