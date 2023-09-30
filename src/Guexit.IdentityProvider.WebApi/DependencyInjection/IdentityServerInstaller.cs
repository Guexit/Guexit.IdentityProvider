﻿using Duende.IdentityServer.Services;
using Guexit.IdentityProvider.Persistence;
using Guexit.IdentityProvider.WebApi.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Guexit.IdentityProvider.WebApi.DependencyInjection;

public static class IdentityServerInstaller
{
    public static IServiceCollection AddIdentityServerAndOperationalData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICorsPolicyService>((container) => {
             var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
             return new DefaultCorsPolicyService(logger)
             {
                 AllowAll = true
             };
        });

        services.AddDbContext<GuexitIdentityDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Guexit_IdentityProvider_IdentityUsers"));
        });

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<GuexitIdentityDbContext>()
            .AddUserManager<GuexitUserManager>()
            .AddDefaultTokenProviders();
        services.ConfigureApplicationCookie(x => x.Cookie.SecurePolicy = CookieSecurePolicy.Always);

        services.AddScoped<GuexitIdentityDbContextMigrator>();
        services.AddOptions<DatabaseOptions>()
            .BindConfiguration(DatabaseOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddIdentityServer(options =>
            {
                options.Authentication.CookieSameSiteMode = SameSiteMode.Lax;
                options.Authentication.CheckSessionCookieSameSiteMode = SameSiteMode.Lax;
            })
            .AddInMemoryClients(configuration.GetSection("IdentityServer:Clients"))
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                {
                    builder.UseNpgsql(
                        configuration.GetConnectionString("Guexit_IdentityProvider_IdentityServerOperationalData"),
                        opt => opt.MigrationsAssembly(typeof(Guexit.IdentityProvider.Persistence.IAssemblyMarker)
                            .Assembly.FullName));
                };

                options.DefaultSchema = "OperationalStore";
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600;
            })
            .AddAspNetIdentity<IdentityUser>();

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.Secure = CookieSecurePolicy.Always;
        });
        
        return services;
    }
}
