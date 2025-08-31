using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApplication.DomainLayer.Entities;

namespace TestApplication.InfrastructureLayer._ُEntitiesConfiguration;

public class FavouriteItemConfiguration : IEntityTypeConfiguration<FavouriteItem>
{
    public void Configure(EntityTypeBuilder<FavouriteItem> builder)
    {
        builder.HasKey(f => new { f.UserId, f.ProductId });
      
    }
}
