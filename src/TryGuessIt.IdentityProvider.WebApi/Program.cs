using Duende.IdentityServer;
using TryGuessIt.IdentityProvider.WebApi.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityServerAndOperationalData(builder.Configuration);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddRazorPages();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
