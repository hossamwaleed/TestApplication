using TestApplication.ApplicationLayer.abstractions;

namespace TestApplication.errors;

public class UserError
{
    public static Error InvalidCredentials = new Error("User.InvalidCredentials", "Invalid Email/Password");
    public static Error InvalidCode = new Error("User.InvalidCode", "Invalid Code");
    public static Error UserNotFound = new Error("User.NotFound", "UserNotound");
    public static Error EmailDuplicated = new Error("User.EmailDuplicated", "EmailDuplicated");
    public static Error DuplicatedConfirmation = new Error("User.DuplicatedConfirmation", "DuplicatedConfirmation");
    public static Error UserEmailExist = new Error("User.EmailExist", "User ");
}
