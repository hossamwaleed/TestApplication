using System.Buffers.Text;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.FavouriteItem;
using TestApplication.Contracts.Product;
using TestApplication.DomainLayer.Entities;
using TestApplication.DomainLayer.IServices;
using TestApplication.errors;
using TestApplication.InfrastructureLayer.Persistence;

namespace TestApplication.InfrastructureLayer.Services;

public class FavouriteItemService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IFavouriteItemService
{
    public  ApplicationDbContext _Context { get; } = context;
    public IHttpContextAccessor HttpContextAccessor { get; } = httpContextAccessor;

    [HttpPost("")]
    public async Task<Result> AddFavouriteItemAsync(int ProductId, string userId)
    {
        var ExistFavouriteItem = await _Context.favouriteItems.AnyAsync(f=>f.UserId == userId & f.ProductId==ProductId);

        if(ExistFavouriteItem) 
            return Result.Failure(FavouriteItemError.FavouriteItemExist);

        var favouriteItem = new FavouriteItem
        {
            UserId = userId,
            ProductId = ProductId,
        };
       await _Context.favouriteItems.AddAsync(favouriteItem);
       await _Context.SaveChangesAsync();

        return Result.Success();
    }
 
    public async Task<Result<IEnumerable<FavouriteItemResponse>>> GetAllAsync(string userid)
    {
              var baseUrl = $"{HttpContextAccessor.HttpContext.Request.Scheme}://" +
             $"{HttpContextAccessor.HttpContext.Request.Host}";


        var FavouriteItems = await _Context.favouriteItems.Where(f => f.UserId == userid)
       .Select(f => new FavouriteItemResponse(
        f.Product.id,
        f.Product.Name,
        f.Product.Description,
        f.Product.hasDiscount,
    f.Product.price,
        f.Product.image,
        $"{baseUrl}/productImage/{f.Product.image}"
    ))



            .AsNoTracking()
            .ToListAsync();

        return Result.Success<IEnumerable<FavouriteItemResponse>>(FavouriteItems);
        
    }
}
