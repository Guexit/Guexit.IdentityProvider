namespace TryGuessIt.IdentityProvider.Messages;

public class UserCreated
{
    public string Id { get; } = default!;

    public UserCreated() 
    {
        
    }

    public UserCreated(string id)
    {
        Id = id;
    }
}
