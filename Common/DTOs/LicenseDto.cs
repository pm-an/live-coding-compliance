namespace Common.DTOs;

public sealed class LicenseDto
{
    public required Guid Id { get; init; }
    public required string LicenseNumber { get; init; }
    public required string StateCode { get; init; }
    public required string StateName { get; init; }
    public required string LineOfAuthority { get; init; }
    public required string Status { get; init; }
    public required DateTime IssuedDate { get; init; }
    public required DateTime ExpirationDate { get; init; }
}
