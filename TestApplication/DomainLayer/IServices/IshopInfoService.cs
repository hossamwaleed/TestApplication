using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.ShopInfo;

namespace TestApplication.DomainLayer.IServices;

public interface IShopService
{
    public  Task<Result> GetShopInfoAsync(ShopInfoRequest request, CancellationToken cancellationToken);
}
