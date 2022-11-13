using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TryGuessIt.IdentityProvider.WebApi.Data;

public class TryGuessItIdentityDbContext : IdentityDbContext
{
	public TryGuessItIdentityDbContext(DbContextOptions<TryGuessItIdentityDbContext> options) 
		: base(options)
	{
	}
}
