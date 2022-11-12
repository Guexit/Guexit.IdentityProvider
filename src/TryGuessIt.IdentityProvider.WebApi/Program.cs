using IdentityServerHost;
using TryGuessIt.IdentityProvider.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddTestUsers(TestUsers.Users);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddRazorPages();

//builder.Services.AddAuthentication()
//    .AddGoogle(options =>
//    {
//        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

//        // register your IdentityServer with Google at https://console.developers.google.com
//        // enable the Google+ API
//        // set the redirect URI to https://localhost:5001/signin-google
//        options.ClientId = "copy client ID from Google here";
//        options.ClientSecret = "copy client secret from Google here";
//    });

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
