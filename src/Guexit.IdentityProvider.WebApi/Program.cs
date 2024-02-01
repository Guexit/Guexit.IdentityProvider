using Duende.IdentityServer;
using Guexit.IdentityProvider.Persistence;
using Guexit.IdentityProvider.WebApi.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.AddTelemetry();
builder.Services.AddMediator();
builder.Services.AddMasstransit(builder.Configuration);
builder.Services.AddIdentityServerAndOperationalData(builder.Configuration, builder.Environment);
builder.Services.AddAuthorization();
builder.Services.AddRazorPages();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.AllowAnyOrigin()));

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.ClientId = builder.Configuration.GetSection("Authentication").GetValue<string>("Google:ClientId")!;
        options.ClientSecret = builder.Configuration.GetSection("Authentication").GetValue<string>("Google:ClientSecret")!;
    })
    .AddDiscord(options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.ClientId = builder.Configuration.GetSection("Authentication").GetValue<string>("Discord:ClientId")!;
        options.ClientSecret = builder.Configuration.GetSection("Authentication").GetValue<string>("Discord:ClientSecret")!;
        options.Scope.Add("guilds");
        options.Scope.Add("email");
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCookiePolicy(new CookiePolicyOptions { Secure = CookieSecurePolicy.Always });
app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

var databaseOptions = app.Services.GetRequiredService<IOptions<DatabaseOptions>>();
if (databaseOptions.Value.MigrateOnStartup)
{
    await using var scope = app.Services.CreateAsyncScope();
    await scope.ServiceProvider.GetRequiredService<GuexitIdentityDbContextMigrator>()
        .MigrateAsync();
}

await app.RunAsync();
