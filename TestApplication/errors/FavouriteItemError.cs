using TestApplication.ApplicationLayer.abstractions;

namespace TestApplication.errors;

public class FavouriteItemError
{
    public static Error FavouriteItemExist = new Error("favouriteIteme.Exist", "This Item Is Already Exist");
}
