using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.Common;
using TestApplication.Contracts.Product;
using TestApplication.DomainLayer.IServices;

namespace TestApplication.Controllers;
[Route("{shopId}/[controller]")]
[ApiController]
[Authorize]
[Consumes("multipart/form-data")]
public class ProductController(IProductService productService) : ControllerBase
{
    public IProductService ProductService { get; } = productService;

    [HttpPost("")]

    public async Task<IActionResult> add(int shopId,[FromForm]ProductRequest request,CancellationToken cancellationToken)
    {
        var result = await ProductService.addproductasync(shopId, request, cancellationToken);

        if (result.IsSuccess)
            return Ok();

        return result.ToProblem(StatusCodes.Status400BadRequest);
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll ([FromRoute]int shopId, [FromQuery]requestFilters filters,CancellationToken cancellationToken)
    {
        var result = await ProductService.GetAllAsync(shopId,filters,cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem(StatusCodes.Status404NotFound);
    }

    [HttpGet("productId")]
    public async Task<IActionResult> GetProduct(int ProductId,int shopId)
    {
        var result = await ProductService.GetproductAsync(ProductId,shopId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem(StatusCodes.Status404NotFound);
    }
}
