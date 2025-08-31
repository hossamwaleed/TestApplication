using TestApplication.ApplicationLayer.abstractions;
using TestApplication.Contracts.FavouriteItem;

namespace TestApplication.DomainLayer.IServices;

public interface IFavouriteItemService
{
    public Task<Result> AddFavouriteItemAsync(int ProductId, string userId);
    public Task<Result<IEnumerable<FavouriteItemResponse>>> GetAllAsync(string userid);

}
