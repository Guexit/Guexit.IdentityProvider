using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TryGuessIt.IdentityProvider.WebApi.Events;
using TryGuessIt.IdentityProvider.WebApi.Events.HandlersForPublish;

namespace TryGuessIt.IdentityProvider.WebApi.UnitTests;

public class WhenHandlingUserCreatedEvent
{
    [Fact]
    public async Task IsPublished()
    {
        var userCreatedEvent = new UserCreatedEvent(Guid.NewGuid().ToString());
        var bus = Substitute.For<IBus>();
        var eventHandler = new UserCreatedEventHandler(
            bus,
            Substitute.For<ILogger<UserCreatedEventHandler>>()
        );

        await eventHandler.Handle(userCreatedEvent, CancellationToken.None);

        await bus.Received(1).Publish(
            Arg.Is<UserCreatedEvent>(x => x.Id == userCreatedEvent.Id),
            Arg.Any<CancellationToken>()
        );
    }
}