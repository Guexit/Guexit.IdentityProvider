using MassTransit;

namespace TryGuessIt.IdentityProvider.WebApi.DependencyInjection;

public static class MasstransitServiceCollectionExtensions
{
    public static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();


            config.AddConsumers(typeof(IAssemblyMarker).Assembly);
            config.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("TryGuessIt_ServiceBus"));
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
