namespace TestApplication.Contracts.Authentication;

public record ConfirmEmailRequest
(
    string UserId,
    string code
    );
