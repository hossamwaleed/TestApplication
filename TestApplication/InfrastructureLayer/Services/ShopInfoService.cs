using Mapster;
using Microsoft.EntityFrameworkCore;
using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.ShopInfo;
using TestApplication.DomainLayer.Entities;
using TestApplication.DomainLayer.IServices;
using TestApplication.errors;
using TestApplication.InfrastructureLayer.Persistence;

namespace TestApplication.InfrastructureLayer.Services;

public class ShopService (ApplicationDbContext context,ILogger<ShopService> logger): IShopService
{
    public ApplicationDbContext Context { get; } = context;
    public ILogger Logger { get; } = logger;

    public async Task<Result> GetShopInfoAsync(ShopInfoRequest request,CancellationToken cancellationToken)
    {
        var isExistingShop = await Context.Shop.AnyAsync(x =>x.Name == request.Name , cancellationToken: cancellationToken);
      
        if(isExistingShop )
            return Result.Failure(ShopError.ShopExist);

        var isExistLocation = await Context.Shop.AnyAsync(x => x.Location.Lat == request.Location.lat && x.Location.Long == request.Location.Long);

        if(isExistLocation )
            return Result.Failure(ShopError.ShopLocationExist);
        if (request.Location is null)
            return Result.Failure(ShopError.ShopLocationRequired);

        var shop = request.Adapt<Shop>();
        shop.address = $"{request.Street}, {request.City}";
        Logger.LogInformation("{long}",shop.Location.Long);
        var addShop = await Context.AddAsync(shop,cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
