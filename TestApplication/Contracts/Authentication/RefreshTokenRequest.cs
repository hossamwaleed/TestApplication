namespace TestApplication.Contracts.Authentication;

public record RefreshTokenRequest
(
    string token,
    string refreshToken
);
