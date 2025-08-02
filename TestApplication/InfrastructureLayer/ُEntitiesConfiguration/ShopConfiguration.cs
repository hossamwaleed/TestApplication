using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApplication.DomainLayer.Entities;

namespace TestApplication.InfrastructureLayer._ُEntitiesConfiguration;

public class ShopConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.OwnsOne(x => x.Location);

    }
}
