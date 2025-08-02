using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.Common;
using TestApplication.Contracts.Product;
using TestApplication.DomainLayer.Entities;
using TestApplication.DomainLayer.IServices;
using TestApplication.errors;
using TestApplication.InfrastructureLayer.Persistence;

namespace TestApplication.InfrastructureLayer.Services;

public class ProductService (ApplicationDbContext context,IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor): IProductService
{
    public ApplicationDbContext Context { get; } = context;
    public IHttpContextAccessor HttpContextAccessor { get; } = httpContextAccessor;

    private readonly string _filePath = $"{webHostEnvironment.WebRootPath}/productImage";


    public async Task<Result> addproductasync(int shopid, [FromForm] ProductRequest request, CancellationToken cancellationtoken)
    {
        var priceAfterDiscount = request.price * .8m;
        var shopExists = await Context.Shop.AnyAsync(s => s.Id == shopid);
        if (!shopExists)
            return Result.Failure(ShopError.ShopNotExist);

        var isexistingproduct = await context.Products.AnyAsync(x => x.Name == request.Name && x.shop.Id == shopid);
        if (isexistingproduct)
            return Result.Failure(ProductError.productExist);

        
        var path = Path.Combine(_filePath, request.image.FileName);
        using var stream = File.Create(path);

        await request.image.CopyToAsync(stream, cancellationtoken);



        var product = request.Adapt<Product>();
        product.ShopId = shopid;
        product.priceAfterDiscount = priceAfterDiscount;
        product.image = request.image.FileName;
        var addProduct = await Context.AddAsync(product,cancellationtoken);

        await Context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<PaginatedList<ProductResponse>>> GetAllAsync([FromRoute]int shopId,requestFilters filters,CancellationToken cancellationToken)
    {
        var ExistShop = await Context.Shop.AnyAsync(x=> x.Id == shopId);
        if(!ExistShop)
            return Result.Failure<PaginatedList<ProductResponse>>(ShopError.ShopNotExist);
        var baseUrl = $"{HttpContextAccessor.HttpContext.Request.Scheme}://" +
             $"{HttpContextAccessor.HttpContext.Request.Host}";

        var products =  Context.Products.Where(p=>p.ShopId==shopId).Select(p=> new ProductResponse(
              p.Name,
        p.Description,
         p.hasDiscount,
           p.price,
           p.image,
          $"{baseUrl}/productImage/{p.image}"
            )).AsNoTracking();
        var response = await PaginatedList<ProductResponse>.CreateAsync(products, filters.PageNumber, filters.PageSize);

        return Result.Success  (response);
    } 
}
