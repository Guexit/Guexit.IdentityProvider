using Guexit.IdentityProvider.Messages;
using MassTransit;
using Mediator;

namespace Guexit.IdentityProvider.WebApi.Events.HandlersForPublish;

public class UserCreatedProducer : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<UserCreatedProducer> _logger;

    public UserCreatedProducer(IPublishEndpoint publishEndpoint, ILogger<UserCreatedProducer> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async ValueTask Handle(UserCreatedDomainEvent @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Handling UserCreatedDomainEvent. UserId: '{userId}'", @event.Id);
        await _publishEndpoint.Publish(new UserCreated(@event.Id, @event.Username), cancellationToken);
    }
}