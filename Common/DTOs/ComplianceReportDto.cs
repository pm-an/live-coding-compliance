namespace Common.DTOs;

public sealed class ComplianceReportDto
{
    public required Guid ProducerId { get; init; }
    public required string ProducerName { get; init; }
    public required string NpnNumber { get; init; }
    public required ComplianceComparisonDto[] Licenses { get; init; }
}

/// <summary>
/// Side-by-side comparison of a single license: our DB record vs external system record.
/// Either side can be null if the license exists only in one system.
/// </summary>
public sealed class ComplianceComparisonDto
{
    public required string StateCode { get; init; }
    public required string LineOfAuthority { get; init; }
    public required LocalLicenseDto? Local { get; init; }
    public required ExternalLicenseDto? External { get; init; }
}

public sealed class LocalLicenseDto
{
    public required Guid Id { get; init; }
    public required string LicenseNumber { get; init; }
    public required string Status { get; init; }
    public required DateTime IssuedDate { get; init; }
    public required DateTime ExpirationDate { get; init; }
}

public sealed class ExternalLicenseDto
{
    public required string CeStatus { get; init; }
    public required int CeHoursRequired { get; init; }
    public required int CeHoursCompleted { get; init; }
    public required DateTime NextRenewalDate { get; init; }
}
