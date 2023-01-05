using MassTransit;
using Mediator;

namespace TryGuessIt.IdentityProvider.WebApi.Events.HandlersForPublish;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IBus _bus;
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(IBus bus, ILogger<UserCreatedEventHandler> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async ValueTask Handle(UserCreatedEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UserCreatedEvent. UserId: '{userId}'", @event.Id);
        await _bus.Publish(new UserCreatedIntegrationEvent(@event.Id), cancellationToken);
    }
}