using System.ComponentModel.DataAnnotations.Schema;

namespace TestApplication.DomainLayer.Entities;

public class Product
{
    public int id { get; set; }

    public string Name { get; set; }= string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public string image { get; set; } = string.Empty;
    public decimal price { get; set; }

    public bool hasDiscount { get; set; }= false;

    public decimal? priceAfterDiscount {  get; set; }
    public int ShopId { get; set; }
    public Shop shop { get; set; }

    
}
