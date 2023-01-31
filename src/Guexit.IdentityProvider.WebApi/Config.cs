using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Guexit.IdentityProvider.WebApi;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources => new[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResource
        {
            Name = IdentityServerConstants.StandardScopes.OfflineAccess
        }
    };
}