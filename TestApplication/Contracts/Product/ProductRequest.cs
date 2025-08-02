namespace TestApplication.Contracts.Product;

public record ProductRequest
(
    
    string Name,
    string Description,
    IFormFile image,
    decimal price,
    bool hasDiscount
  
    
    ); 
