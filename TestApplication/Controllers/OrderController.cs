using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.order;
using TestApplication.DomainLayer.IServices;

namespace TestApplication.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize]
public class OrderController(IOrderService orderService) : ControllerBase
{
    public IOrderService OrderService { get; } = orderService;

    [HttpPost("")]

    public async Task<IActionResult> AddOrder([FromBody] OrderRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await OrderService.AddOrderAsync(userId, request, cancellationToken);
        return result.IsSuccess ? Ok("order is Ready") : result.ToProblem(StatusCodes.Status400BadRequest);
    }
}
