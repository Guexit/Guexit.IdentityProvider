using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Guexit.IdentityProvider.Persistence;

public class GuexitIdentityDbContext : IdentityDbContext
{
    public GuexitIdentityDbContext(DbContextOptions<GuexitIdentityDbContext> options)
        : base(options)
    {
    }
}