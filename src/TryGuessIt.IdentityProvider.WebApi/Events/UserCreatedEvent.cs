using Mediator;

namespace TryGuessIt.IdentityProvider.WebApi.Events;

public sealed class UserCreatedEvent : INotification
{
    public string Id { get; }

	public UserCreatedEvent(string id)
	{
        Id = id;
    }
}
