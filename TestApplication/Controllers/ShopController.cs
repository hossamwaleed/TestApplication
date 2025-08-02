using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.ShopInfo;
using TestApplication.DomainLayer.IServices;

namespace TestApplication.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize]
public class ShopController (IShopService shopService): ControllerBase
{
    public IShopService ShopService { get; } = shopService;

    [HttpPost("")]

    public async Task<IActionResult> AddShop(ShopInfoRequest request,CancellationToken cancellationToken)
    {
        var result = await ShopService.GetShopInfoAsync(request, cancellationToken);

    return  result.IsSuccess ? Ok() : result.ToProblem(StatusCodes.Status400BadRequest);
    }
}
