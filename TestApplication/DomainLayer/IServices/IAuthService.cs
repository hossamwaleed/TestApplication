using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.Authentication;

namespace TestApplication.DomainLayer.IServices;

public interface IAuthService
{
    public Task<Result<LogInResponse>>  GetTokenAsync(loginRequest request,CancellationToken cancellationToken);
    public  Task<Result> GetRegisterAsync(SignUpRequest request, CancellationToken cancellationToken);
    public Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
    public  Task<Result<LogInResponse>> GetRefreshToken(string Token, string RefreshToken, CancellationToken cancellationToken);
}
