using Microsoft.AspNetCore.Mvc;
using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.Common;
using TestApplication.Contracts.Product;

namespace TestApplication.DomainLayer.IServices;

public interface IProductService
{
    Task<Result> addproductasync(int shopid, [FromForm] ProductRequest request, CancellationToken cancellationtoken);
    Task<Result<PaginatedList<ProductResponse>>> GetAllAsync(int shopId,requestFilters filters ,CancellationToken cancellationToken);
}
