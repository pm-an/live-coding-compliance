namespace Common.DTOs;

public sealed class ProducerDto
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string NpnNumber { get; init; }
    public required string Email { get; init; }
    public required int ActiveLicenseCount { get; init; }
    public required DateTime CreatedAt { get; init; }
}
