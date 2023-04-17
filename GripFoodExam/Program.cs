using GripFoodExam.Entities;
using GripFoodExam.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<GripFoodDbContext>(Q =>
{
    Q.UseSqlite("Data Source=local.db");
});

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore().UseDbContext<GripFoodDbContext>();
    })
    .AddServer(options =>
    {
        options.SetAuthorizationEndpointUris(OpenIdSettings.Endpoints.Authorization)
                .SetTokenEndpointUris(OpenIdSettings.Endpoints.Token)
                .SetIntrospectionEndpointUris(OpenIdSettings.Endpoints.Introspection)
                .SetUserinfoEndpointUris(OpenIdSettings.Endpoints.Userinfo)
                .SetRevocationEndpointUris(OpenIdSettings.Endpoints.Revoke)
                .SetLogoutEndpointUris(OpenIdSettings.Endpoints.Logout);
        options.AllowClientCredentialsFlow(); //humanless login
        options.AllowAuthorizationCodeFlow(); //human login
        options.AllowRefreshTokenFlow(); //human login
        options.RegisterClaims(OpenIdSettings.Claims);
        options.RegisterScopes(OpenIdSettings.Scopes);
        options.AddDevelopmentEncryptionCertificate();
        options.AddDevelopmentSigningCertificate();
        options.UseAspNetCore()
                .DisableTransportSecurityRequirement()
                .EnableAuthorizationEndpointPassthrough()
                .EnableTokenEndpointPassthrough()
                .EnableUserinfoEndpointPassthrough()
                .EnableLogoutEndpointPassthrough();
        options.UseDataProtection();
        options.UseReferenceAccessTokens()
            .UseReferenceRefreshTokens();
        options.SetAccessTokenLifetime(TimeSpan.FromHours(24));
        options.SetRefreshTokenLifetime(TimeSpan.FromDays(30));
        options.SetRefreshTokenReuseLeeway(TimeSpan.FromSeconds(60));
    })
    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.EnableAuthorizationEntryValidation();
        options.EnableTokenEntryValidation();
        options.UseAspNetCore();
        options.UseDataProtection();
    });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<AutomaticMigrationService>();
    builder.Services.AddHostedService<SetupDevelopmentEnvironmentHostedService>();
}

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<GripFoodDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
