namespace Guexit.IdentityProvider.Messages;

public class UserCreated
{
    public string Id { get; set; } = default!;
    public string Username { get; set; } = default!;

    public UserCreated() { }

    public UserCreated(string id, string username)
    {
        Id = id;
        Username = username;
    }
}
