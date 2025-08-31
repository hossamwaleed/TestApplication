using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.order;

namespace TestApplication.DomainLayer.IServices;

public interface IOrderService
{
    public Task<Result> AddOrderAsync(string UserId, OrderRequest request, CancellationToken cancellationToken);
}
