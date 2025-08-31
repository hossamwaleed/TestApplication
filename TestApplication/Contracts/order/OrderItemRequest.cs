using TestApplication.DomainLayer.Entities;

namespace TestApplication.Contracts.order;

public record OrderItemRequest
(
   
 int ProductId,
 int quantity
    );
