using Domain.Models;
using Infrastructure.Authorization.Meta;

namespace Infrastructure.Authorization.Fakes;

public sealed class FakeAuthorizationAccessor : IAuthorizationAccessor
{
    public ApplicationUser User { get; } = new()
    {
        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        Email = "test-user@example.com"
    };
}
