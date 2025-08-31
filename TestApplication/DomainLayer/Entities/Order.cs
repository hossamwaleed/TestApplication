namespace TestApplication.DomainLayer.Entities;

public class Order
{
    public int id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string Status { get; set; } = "Pending";


  

    public ApplicationUser User { get; set; }
    public ICollection<OrderItem> orderItems { get; set; }
}
