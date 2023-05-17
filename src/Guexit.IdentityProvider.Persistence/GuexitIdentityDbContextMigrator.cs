using Duende.IdentityServer.EntityFramework.DbContexts;
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
    private readonly GuexitIdentityDbContext _identityDbContext;
    private readonly PersistedGrantDbContext _persistedGrantDbContext;
    private readonly ILogger<GuexitIdentityDbContextMigrator> _logger;

    public GuexitIdentityDbContextMigrator(GuexitIdentityDbContext dbContext, PersistedGrantDbContext persistedGrantDbContext, ILogger<GuexitIdentityDbContextMigrator> logger)
    {
        _identityDbContext = dbContext;
        _persistedGrantDbContext = persistedGrantDbContext;
        _logger = logger;
    }

    public async Task MigrateAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Starting migrations...");
        await _identityDbContext.Database.MigrateAsync(ct);
        await _persistedGrantDbContext.Database.MigrateAsync(ct);
        _logger.LogInformation("Database migrations applied");
    }
}
