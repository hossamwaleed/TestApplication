using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApplication.ApplicationLayer.abstractions;
using TestApplication.DomainLayer.IServices;

namespace TestApplication.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize]
public class FavouriteItemController (IFavouriteItemService favouriteItemService): ControllerBase
{
    public IFavouriteItemService FavouriteItemService { get; } = favouriteItemService;

    [HttpPost]

    public async Task<IActionResult> AddFavouriteItem([FromBody] int ProductId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await FavouriteItemService.AddFavouriteItemAsync(ProductId, userId);
        if(result.IsSuccess)
            return Ok(new { message = "Added to favorites successfully" });

        return result.ToProblem(StatusCodes.Status400BadRequest);
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var result = await FavouriteItemService.GetAllAsync(userId);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem(StatusCodes.Status400BadRequest);
    }
}
