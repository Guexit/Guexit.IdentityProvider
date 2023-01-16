using MassTransit;
using Mediator;
using TryGuessIt.IdentityProvider.Messages;

namespace TryGuessIt.IdentityProvider.WebApi.Events.HandlersForPublish;

public class UserCreatedEventHandlerForPublish : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IBus _bus;
    private readonly ILogger<UserCreatedEventHandlerForPublish> _logger;

    public UserCreatedEventHandlerForPublish(IBus bus, ILogger<UserCreatedEventHandlerForPublish> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async ValueTask Handle(UserCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UserCreatedDomainEvent. UserId: '{userId}'", @event.Id);
        await _bus.Publish(new UserCreated(@event.Id, @event.Username), cancellationToken);
    }
}