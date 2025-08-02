using Mapster;
using TestApplication.Contracts.ShopInfo;
using TestApplication.DomainLayer.Entities;

namespace TestApplication.ApplicationLayer.abstractions;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ShopInfoRequest,Shop>().Map(dest=>dest.Location, src=> new ShopLocation { Lat=src.Location.lat,Long=src.Location.Long});
    }
}
