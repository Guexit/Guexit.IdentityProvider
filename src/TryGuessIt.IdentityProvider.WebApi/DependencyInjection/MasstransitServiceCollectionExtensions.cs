using MassTransit;

namespace TryGuessIt.IdentityProvider.WebApi.DependencyInjection;

public static class MasstransitServiceCollectionExtensions
{
    public static IServiceCollection AddMasstransit(this IServiceCollection services)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            config.AddConsumers(typeof(IAssemblyMarker).Assembly);
            config.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}
