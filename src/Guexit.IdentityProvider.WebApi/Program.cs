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
    .AddFacebook(options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.AppId = builder.Configuration.GetSection("Authentication").GetValue<string>("Facebook:ClientId")!;
        options.AppSecret = builder.Configuration.GetSection("Authentication").GetValue<string>("Facebook:ClientSecret")!;
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
