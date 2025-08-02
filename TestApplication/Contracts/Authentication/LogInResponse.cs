namespace TestApplication.Contracts.Authentication;

public record LogInResponse
(
   string email,
    string password,
    string FirstName,
    string Id,

    string Token,
  
       int ExpiresIn,
      string RefreshToken
    );
