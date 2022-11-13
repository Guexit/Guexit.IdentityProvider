using IdentityServerHost;
using Microsoft.EntityFrameworkCore;

namespace TryGuessIt.IdentityProvider.WebApi.DependencyInjection;

public static class IdentityServerServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServerAndOperationalData(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddIdentityServer()
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
            .AddTestUsers(TestUsers.Users)
            .Services;
    }
}
