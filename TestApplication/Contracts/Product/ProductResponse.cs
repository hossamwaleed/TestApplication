namespace TestApplication.Contracts.Product;

public record ProductResponse
(
    string Name,
    string Description,    
       bool hasDiscount,
    decimal price,
    string image,
     string ImageUrl
    );
