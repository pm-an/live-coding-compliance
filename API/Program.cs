using Application.Actions.Producers.Queries;
using Infrastructure.DependencyInjection;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure();
builder.Services.AddHandlers(typeof(GetProducers).Assembly);

builder.Services.AddAuthentication("FakeScheme")
    .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>(
        "FakeScheme", options => { });
builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    SeedData.Initialize(dbContext);
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public class FakeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public FakeAuthenticationHandler(
        Microsoft.Extensions.Options.IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        System.Text.Encodings.Web.UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "test-user") };
        var identity = new System.Security.Claims.ClaimsIdentity(claims, "FakeScheme");
        var principal = new System.Security.Claims.ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "FakeScheme");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
