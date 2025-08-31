namespace TestApplication.Contracts.order;

public record OrderRequest
(
    
    List<OrderItemRequest> order
    );