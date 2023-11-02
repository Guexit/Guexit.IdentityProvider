using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Guexit.IdentityProvider.Persistence;

public class GuexitIdentityDbContext : IdentityDbContext, IDataProtectionKeyContext
{
    public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = default!;

    public GuexitIdentityDbContext(DbContextOptions<GuexitIdentityDbContext> options)
        : base(options)
    {
    }
}