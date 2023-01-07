using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TryGuessIt.IdentityProvider.Messages;
using TryGuessIt.IdentityProvider.WebApi.Events;
using TryGuessIt.IdentityProvider.WebApi.Events.HandlersForPublish;

namespace TryGuessIt.IdentityProvider.WebApi.UnitTests;

public class WhenHandlingUserCreatedEvent
{
    [Fact]
    public async Task IsPublished()
    {
        var userCreated = new UserCreatedDomainEvent(Guid.NewGuid().ToString());
        var bus = Substitute.For<IBus>();
        var eventHandler = new UserCreatedEventHandlerForPublish(
            bus,
            Substitute.For<ILogger<UserCreatedEventHandlerForPublish>>()
        );

        await eventHandler.Handle(userCreated, CancellationToken.None);

        await bus.Received(1).Publish(
            Arg.Is<UserCreated>(x => x.Id == userCreated.Id),
            Arg.Any<CancellationToken>()
        );
    }
}