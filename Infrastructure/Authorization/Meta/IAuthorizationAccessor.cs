using Domain.Models;

namespace Infrastructure.Authorization.Meta;

public interface IAuthorizationAccessor
{
    ApplicationUser User { get; }
}
