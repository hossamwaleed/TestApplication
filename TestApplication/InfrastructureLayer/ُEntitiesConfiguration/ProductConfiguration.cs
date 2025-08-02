 using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApplication.DomainLayer.Entities;

namespace TestApplication.InfrastructureLayer._ُEntitiesConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p=>p.id);
 

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.price)
       .HasColumnType("decimal(18,2)");
        builder.HasIndex(n=>n.Name).IsUnique();
        builder.Property(p => p.priceAfterDiscount)
          .HasColumnType("decimal(18,2)");

        builder.HasOne(p => p.shop)
           .WithMany(s => s.Products)
           .OnDelete(DeleteBehavior.Cascade);


    }
}
