using Guexit.IdentityProvider.Messages;
using Guexit.IdentityProvider.WebApi.Events;
using Guexit.IdentityProvider.WebApi.Events.HandlersForPublish;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Guexit.IdentityProvider.WebApi.UnitTests;

public class WhenHandlingUserCreatedEvent
{
    [Fact]
    public async Task IsPublished()
    {
        var userCreated = new UserCreatedDomainEvent(Guid.NewGuid().ToString(), "username");
        var bus = Substitute.For<IBus>();
        var eventHandler = new UserCreatedEventHandlerForPublish(
            bus,
            Substitute.For<ILogger<UserCreatedEventHandlerForPublish>>()
        );

        await eventHandler.Handle(userCreated, CancellationToken.None);

        await bus.Received(1).Publish(
            Arg.Is<UserCreated>(x => x.Id == userCreated.Id && x.Username == userCreated.Username),
            Arg.Any<CancellationToken>()
        );
    }
}