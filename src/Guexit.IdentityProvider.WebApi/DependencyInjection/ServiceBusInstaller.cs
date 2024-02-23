using Guexit.IdentityProvider.Persistence;
using MassTransit;

namespace Guexit.IdentityProvider.WebApi.DependencyInjection;

public static class ServiceBusInstaller
{
    public static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.AddEntityFrameworkOutbox<GuexitIdentityDbContext>(outboxOptions =>
            {
                outboxOptions.UsePostgres();
                outboxOptions.UseBusOutbox();
            });
            
            config.SetKebabCaseEndpointNameFormatter();
            
            config.AddConsumers(typeof(IAssemblyMarker).Assembly);
            
            config.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("Guexit_ServiceBus"));
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
