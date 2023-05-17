using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Guexit.IdentityProvider.Persistence;

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";

    public bool MigrateOnStartup { get; init; } = false;
}


public sealed class GuexitIdentityDbContextMigrator
{
    private readonly GuexitIdentityDbContext _context;
    private readonly ILogger<GuexitIdentityDbContextMigrator> _logger;

    public GuexitIdentityDbContextMigrator(GuexitIdentityDbContext dbContext, ILogger<GuexitIdentityDbContextMigrator> logger)
    {
        _context = dbContext;
        _logger = logger;
    }

    public async Task MigrateAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Starting migrations...");
        await _context.Database.MigrateAsync(ct);
        _logger.LogInformation("Database migrations applied");
    }
}
