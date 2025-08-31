namespace TestApplication.Contracts.FavouriteItem;

public record FavouriteItemResponse
(
    int productId,
   string Name,
    string Description,
       bool hasDiscount,
    decimal price,
    string image,
    string imageUrl
    
    );
