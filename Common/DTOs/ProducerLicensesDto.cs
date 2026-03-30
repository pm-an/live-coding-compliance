namespace Common.DTOs;

public sealed class ProducerLicensesDto
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string NpnNumber { get; init; }
    public required LicenseDto[] Licenses { get; init; }
}
