using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Contracts.Authentication;
using TestApplication.DomainLayer.IServices;

namespace TestApplication.Controllers;
[Route("[controller]")]
[ApiController]

public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService AuthService = authService;
    [HttpPost]

    public async Task<IActionResult> Login(loginRequest request, CancellationToken cancellationToken)
    {
        var authResult = await AuthService.GetTokenAsync(request, cancellationToken);

        if (authResult.IsSuccess)
            return Ok(authResult);

        return BadRequest(authResult);
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request, CancellationToken cancellationToken)
    {

        var Authresult = await AuthService.GetRefreshToken(request.token, request.refreshToken, cancellationToken);

        return Authresult.IsSuccess ? Ok(Authresult.Value) : BadRequest(Authresult.Error);



    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] SignUpRequest request, CancellationToken cancellationToken)
    {

        var result = await AuthService.GetRegisterAsync(request, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {

        var result = await AuthService.ConfirmEmailAsync(request);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
