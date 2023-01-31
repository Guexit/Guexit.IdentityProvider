using Mediator;

namespace Guexit.IdentityProvider.WebApi.Events;

public sealed class UserCreatedDomainEvent : INotification
{
    public string Id { get; }
    public string Username { get; }

	public UserCreatedDomainEvent(string id, string username)
	{
        Id = id;
        Username = username;
    }
}
