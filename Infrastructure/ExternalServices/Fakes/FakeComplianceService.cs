using Infrastructure.ExternalServices.Meta;

namespace Infrastructure.ExternalServices.Fakes;

/// <summary>
/// Fake implementation that returns hardcoded compliance data for known producers.
/// In production, this would call an external XML/SOAP API and deserialize the response.
/// </summary>
public sealed class FakeComplianceService : IComplianceService
{
    public Task<ComplianceResponse?> GetComplianceStatusAsync(string npnNumber, CancellationToken cancellationToken = default)
    {
        ComplianceResponse? response = npnNumber switch
        {
            "1234567" => new("1234567",
            [
                new("TX", "Property and Casualty", "Compliant", 24, 24, new DateTime(2027, 3, 15)),
                new("CA", "Life", "Deficient", 20, 12, new DateTime(2026, 9, 30)),
                new("NY", "Property and Casualty", "Expired", 15, 0, new DateTime(2025, 12, 31)),
                new("FL", "Life", "Compliant", 24, 20, new DateTime(2027, 6, 30)),
            ]),

            "2345678" => new("2345678",
            [
                new("TX", "Life", "Compliant", 24, 24, new DateTime(2027, 1, 15)),
                new("CA", "Property and Casualty", "Compliant", 20, 20, new DateTime(2027, 4, 30)),
                new("IL", "Life", "Pending", 30, 0, new DateTime(2027, 12, 31)),
            ]),

            "3456789" => new("3456789",
            [
                new("NY", "Property and Casualty", "Compliant", 15, 15, new DateTime(2027, 8, 31)),
                new("FL", "Property and Casualty", "Deficient", 24, 16, new DateTime(2026, 11, 30)),
            ]),

            "4567890" => new("4567890",
            [
                new("TX", "Property and Casualty", "Compliant", 24, 24, new DateTime(2027, 5, 15)),
                new("CA", "Life", "Expired", 20, 8, new DateTime(2025, 6, 30)),
                new("NY", "Life", "Compliant", 15, 15, new DateTime(2027, 9, 30)),
            ]),

            _ => null
        };

        return Task.FromResult(response);
    }
}
