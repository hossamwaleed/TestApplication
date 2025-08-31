using Mapster;
using Microsoft.EntityFrameworkCore;
using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.order;
using TestApplication.DomainLayer.Entities;
using TestApplication.DomainLayer.IServices;
using TestApplication.errors;
using TestApplication.InfrastructureLayer.Persistence;

namespace TestApplication.InfrastructureLayer.Services;

public class OrderService (ApplicationDbContext context): IOrderService
{
    public ApplicationDbContext Context { get; } = context;
  //  public ILogger _Logger { get; } = Logger;

    public async Task<Result> AddOrderAsync (string UserId,OrderRequest request,CancellationToken cancellationToken)

    {
        bool hasStockIssue = false;
     

        if (request.order.Any(x => x.quantity <= 0))
            return Result.Failure(OrderError.quantityValueWrong);

        foreach(var item in request.order)
        {
            var product = await Context.Products.FindAsync(item.ProductId);
            if (product == null)
                return Result.Failure(ProductError.productNotExist);

            if(product.Stock < item.quantity)
                hasStockIssue = true;
          //  _Logger.LogInformation("Stock: {stock}, Quantity: {qty}", product.Stock, request.order);
            throw new Exception($"Not enough stock for product {item.ProductId}");
        }
        var order = request.Adapt<Order>();

        
        foreach(var item in order.orderItems)
        {
            var product = await Context.Products.FindAsync(item.ProductId);
            product.Stock -= item.quantity;
        }
        order.Status = hasStockIssue ? "PostPoned" : "Completed";
        await  Context.orders.AddAsync(order);
        await Context.SaveChangesAsync();

        return Result.Success();
    }
    
}
