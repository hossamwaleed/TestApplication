using System.Reflection;
using System.Text;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using TestApplication.Authentication;
using TestApplication.DomainLayer.Entities;
using TestApplication.DomainLayer.IServices;
using TestApplication.InfrastructureLayer.Persistence;
using TestApplication.InfrastructureLayer.Services;

namespace TestApplication;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services,IConfiguration configuration) 
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddScoped<IShopService,ShopService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddHttpContextAccessor();
        var ConnectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("ConnectionString DefaultConnection NotFound");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));
     
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        services.addMapsterConfig();
        services.AuthConfig( configuration);
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
      
        return services;
    }
    public static IServiceCollection AuthConfig(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        services.AddAuthentication();
        services.AddOptions<JwtOptions>().BindConfiguration(JwtOptions.JwtSectionName).ValidateDataAnnotations().ValidateOnStart();
        var jwtSettings = Configuration.GetSection(JwtOptions.JwtSectionName).Get<JwtOptions>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience=true,
             ValidateIssuer = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Key)),
             ValidAudience = jwtSettings.Audiance,
             ValidIssuer = jwtSettings.Issuer,

            };
        });
       
        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.Configure<IdentityOptions>(options =>
        {

            options.User.AllowedUserNameCharacters =
         "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        });

        return services;

    }
    public static IServiceCollection addMapsterConfig(this IServiceCollection services)
    {

        var MappingConfig = TypeAdapterConfig.GlobalSettings;
        MappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(MappingConfig));
        return services;
    }
    public static IServiceCollection addSwaggerServices(this IServiceCollection services)
    {
        // Learn more about configuring Swagger / OpenAPI at https://aka.ms/aspnetcore/swashbuckle
       

        return services;
    }
}
