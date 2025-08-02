using TestApplication.ApplicationLayer.abstractions;

namespace TestApplication.errors;

public class ShopError
{
    public static Error ShopExist = new Error("Shop.Exist", "Usthis shop is exist already ");
    public static Error ShopNotExist = new Error("ShopNot.Exist", "Usthis shop is exist already ");
    public static Error ShopLocationExist = new Error("Shop.LocationExist", "Usthis shopLocation is exist already ");
    public static Error ShopLocationRequired = new Error("Shop.LocationRequired", "ShopLocationIsrequired ");
}
