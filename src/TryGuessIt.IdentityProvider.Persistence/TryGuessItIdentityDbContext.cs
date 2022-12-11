using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TryGuessIt.IdentityProvider.Persistence;

public class TryGuessItIdentityDbContext : IdentityDbContext
{
    public TryGuessItIdentityDbContext(DbContextOptions<TryGuessItIdentityDbContext> options)
        : base(options)
    {
    }
}