using System.Security.Cryptography;
using System.Text;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Authentication;
using TestApplication.Contracts.Authentication;
using TestApplication.DomainLayer.Entities;
using TestApplication.DomainLayer.IServices;
using TestApplication.errors;

namespace TestApplication.InfrastructureLayer.Services;

public class AuthService (UserManager<ApplicationUser>userManager, IJwtProvider jwtProvider,ILogger<AuthService>logger): IAuthService
    
{
    private  UserManager<ApplicationUser> _userManager { get; } = userManager;
    public IJwtProvider JwtProvider { get; } = jwtProvider;
    public ILogger<AuthService> Logger { get; } = logger;

    public async Task<Result<LogInResponse>> GetTokenAsync(loginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.email);

        if (user is null)
            return Result.Failure<LogInResponse>(UserError.EmailDuplicated);

        var checkPassword = await _userManager.CheckPasswordAsync(user,request.password);
        if(!checkPassword)
            return Result.Failure<LogInResponse>(UserError.InvalidCredentials);

        var (teken,ExpiresIn) = JwtProvider.GenerateToken(user);
        var refreshtoken = GenerateRefreshToken();

        var userResponse = new LogInResponse(Guid.NewGuid().ToString(), user.Email, user.FirstName, user.LastName, teken, ExpiresIn * 60, refreshtoken);
        return Result.Success(userResponse);
    }
    public async Task<Result<LogInResponse>> GetRefreshToken(string Token, string RefreshToken, CancellationToken cancellationToken)
    {
        var userId = JwtProvider.validateToken(Token);
        if (userId is null)
            return Result.Failure<LogInResponse>(UserError.UserNotFound);

        var user= await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure<LogInResponse>(UserError.UserNotFound);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.isActive);

        if (userRefreshToken is null)
            return Result.Failure<LogInResponse>(UserError.UserNotFound);

        userRefreshToken.RevokedOn = DateTime.UtcNow;

        var (newToken, expiresIn) = JwtProvider.GenerateToken(user);
        var NewRefreshToken = GenerateRefreshToken();
        var RefreshTokenExpiration = DateTime.UtcNow.AddDays(14);
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newToken,
            ExpiresOn = RefreshTokenExpiration
        });
        await _userManager.UpdateAsync(user);
        var userResponse = new LogInResponse(Guid.NewGuid().ToString(), user.Email, user.FirstName, user.LastName, newToken, expiresIn * 60, NewRefreshToken);
        return Result.Success(userResponse);
    }
    public async Task<Result> GetRegisterAsync(SignUpRequest request,CancellationToken cancellationToken)
    {
        var code = string.Empty;
        var emailIsExist = await _userManager.Users.AnyAsync(x =>x.Email==request.Email, cancellationToken: cancellationToken);
        if (emailIsExist)
            return Result.Failure(UserError.EmailDuplicated);
        var user = request.Adapt<ApplicationUser>();
        var account = await _userManager.CreateAsync(user, request.password);
        if(account.Succeeded)
        {
             code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code =WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            Logger.LogInformation("Verification Code :{code}", code);
            return Result.Success();
        }
     
        var error = account.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description));
    }
    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return Result.Failure(UserError.UserNotFound);

        if (user.EmailConfirmed)
            return Result.Failure(UserError.DuplicatedConfirmation);

        var code = request.code;
        try
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }
        catch (FormatException)
        {
            Result.Failure(UserError.InvalidCode);
        }
        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (result.Succeeded)
        {
            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description));
    }
    
    public static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
