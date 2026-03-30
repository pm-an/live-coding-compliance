namespace Domain.Models;

public sealed class ApplicationUser
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
}
