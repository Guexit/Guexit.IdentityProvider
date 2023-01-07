using Mediator;

namespace TryGuessIt.IdentityProvider.WebApi.Events;

public sealed class UserCreatedDomainEvent : INotification
{
    public string Id { get; }

	public UserCreatedDomainEvent(string id)
	{
        Id = id;
    }
}
