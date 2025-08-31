using Mapster;
using Microsoft.AspNetCore.Identity.Data;
using TestApplication.Contracts.Authentication;
using TestApplication.Contracts.FavouriteItem;
using TestApplication.DomainLayer.Entities;

namespace TestApplication;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SignUpRequest, ApplicationUser>().Map(dest => dest.UserName, src => src.Email);
        config
    .NewConfig<FavouriteItem, FavouriteItemResponse>()
    .Map(dest => dest.productId, src => src.ProductId)
    .Map(dest => dest.Name, src => src.Product.Name)
    .Map(dest => dest.Description, src => src.Product.Description)
    .Map(dest => dest.hasDiscount, src => src.Product.hasDiscount)
    .Map(dest => dest.price, src => src.Product.price)
    .Map(dest => dest.image, src => src.Product.image); // لو عندك عمود اسمه Image
    
    }
}
