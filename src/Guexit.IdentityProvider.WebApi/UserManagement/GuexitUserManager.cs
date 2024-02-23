using Guexit.IdentityProvider.Persistence;
using Guexit.IdentityProvider.WebApi.Events;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Guexit.IdentityProvider.WebApi.UserManagement;

public sealed class GuexitUserStore : UserStore<IdentityUser>
{
    private readonly IPublisher _publisher;


    public GuexitUserStore(GuexitIdentityDbContext dbContext, IPublisher publisher) : base(dbContext)
    {
        _publisher = publisher;
    }

    public override async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken = new CancellationToken())
    {
        await _publisher.Publish(new UserCreatedDomainEvent(user.Id, user.Email), cancellationToken);
        return await base.CreateAsync(user, cancellationToken);
    }
}
