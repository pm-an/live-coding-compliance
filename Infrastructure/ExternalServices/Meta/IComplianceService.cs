namespace Infrastructure.ExternalServices.Meta;

/// <summary>
/// External compliance service that returns continuing education (CE) status for a producer.
/// Under the hood this integrates with an XML/SOAP API, but the service abstracts that away.
/// Returns null if the producer is not found in the external system.
/// </summary>
public interface IComplianceService
{
    Task<ComplianceResponse?> GetComplianceStatusAsync(string npnNumber, CancellationToken cancellationToken = default);
}

public sealed record ComplianceResponse(string Npn, ComplianceLicenseEntry[] Licenses);

public sealed record ComplianceLicenseEntry(
    string StateCode,
    string LineOfAuthority,
    string CeStatus,
    int CeHoursRequired,
    int CeHoursCompleted,
    DateTime NextRenewalDate);
