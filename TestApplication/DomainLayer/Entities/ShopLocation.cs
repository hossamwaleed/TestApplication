using Microsoft.EntityFrameworkCore;

namespace TestApplication.DomainLayer.Entities;
[Owned]
public sealed class ShopLocation
{
    public double Lat { get; set; }
    public double Long { get; set; }
}
