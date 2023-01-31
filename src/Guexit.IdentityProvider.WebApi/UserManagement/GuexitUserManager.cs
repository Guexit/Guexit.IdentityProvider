using Guexit.IdentityProvider.WebApi.Events;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Guexit.IdentityProvider.WebApi.UserManagement;

public sealed class GuexitUserManager : UserManager<IdentityUser>
{
    private readonly IPublisher _publisher;

    public GuexitUserManager(
        IUserStore<IdentityUser> store, 
        IOptions<IdentityOptions> optionsAccessor, 
        IPasswordHasher<IdentityUser> passwordHasher, 
        IEnumerable<IUserValidator<IdentityUser>> userValidators, 
        IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
        ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, 
        IServiceProvider services, 
        ILogger<UserManager<IdentityUser>> logger, 
        IPublisher publisher) 
        : base(store, optionsAccessor, passwordHasher, userValidators,
            passwordValidators, keyNormalizer, errors, services, logger)
    {
        _publisher = publisher;
    }

    public override async Task<IdentityResult> CreateAsync(IdentityUser user)
    {
        var identityResult = await base.CreateAsync(user);
        if (identityResult.Succeeded)
            await _publisher.Publish(new UserCreatedDomainEvent(user.Id, user.Email));

        return identityResult;
    }
}
