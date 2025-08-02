using Mapster;
using Microsoft.AspNetCore.Identity.Data;
using TestApplication.Contracts.Authentication;
using TestApplication.DomainLayer.Entities;

namespace TestApplication;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SignUpRequest, ApplicationUser>().Map(dest => dest.UserName, src => src.Email);
    }
}
