namespace TestApplication.Contracts.Authentication;

public record SignUpRequest
(
    string Email,
    string password,
    string mobileNumber,
    string FirstName,
    string LastName
    );
