

using TestApplication.ApplicationLayer.abstractions;

namespace TestApplication.errors;

public class ProductError
{
    public static Error productExist = new Error("Shop.Exist", "Usthis shop is exist already ");
}
