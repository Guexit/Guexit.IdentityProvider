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
        var publishEndpoint = Substitute.For<IPublishEndpoint>();
        var eventHandler = new UserCreatedProducer(
            publishEndpoint,
            Substitute.For<ILogger<UserCreatedProducer>>()
        );

        await eventHandler.Handle(userCreated);

        await publishEndpoint.Received(1).Publish(
            Arg.Is<UserCreated>(x => x.Id == userCreated.Id && x.Username == userCreated.Username),
            Arg.Any<CancellationToken>()
        );
    }
}