using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TryGuessIt.IdentityProvider.Persistence;

namespace TryGuessIt.IdentityProvider.WebApi.DependencyInjection;

public static class IdentityServerServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServerAndOperationalData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICorsPolicyService>((container) => {
             var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
             return new DefaultCorsPolicyService(logger)
             {
                 AllowAll = true
             };
        });

        services.AddDbContext<TryGuessItIdentityDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("TryGuessIt_IdentityProvider_IdentityUsers"));
        });

        services.AddIdentity<IdentityUser, IdentityRole>()
           .AddEntityFrameworkStores<TryGuessItIdentityDbContext>()
           .AddDefaultTokenProviders();

        services.AddIdentityServer()
            .AddInMemoryClients(configuration.GetSection("IdentityServer:Clients"))
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseNpgsql(
                        configuration.GetConnectionString("TryGuessIt_IdentityProvider_IdentityServerOperationalData"),
                        opt => opt.MigrationsAssembly(typeof(IAssemblyMarker).Assembly.FullName));

                options.DefaultSchema = "OperationalStore";
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600;
            })
            .AddAspNetIdentity<IdentityUser>();

        return services;
    }
}
