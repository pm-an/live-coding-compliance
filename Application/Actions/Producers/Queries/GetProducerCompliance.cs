using Common.DTOs;
using Infrastructure.CQRS.Meta;
using Infrastructure.ExternalServices.Meta;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Actions.Producers.Queries;

public static class GetProducerCompliance
{
    public sealed record Query(Guid Id) : IRequest<ComplianceReportDto?>;

    public sealed class Handler : IRequestHandler<Query, ComplianceReportDto?>
    {
        private readonly AppDbContext _dbContext;
        private readonly IComplianceService _complianceService;

        public Handler(AppDbContext dbContext, IComplianceService complianceService)
        {
            _dbContext = dbContext;
            _complianceService = complianceService;
        }

        public async Task<ComplianceReportDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var producer = await _dbContext.Producers
                .Include(p => p.Licenses)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (producer is null)
                return null;

            var compliance = await _complianceService.GetComplianceStatusAsync(
                producer.NpnNumber, cancellationToken);

            var externalLookup = compliance?.Licenses
                .ToDictionary(l => (l.StateCode, l.LineOfAuthority))
                ?? [];

            var comparisons = producer.Licenses!.Select(license =>
            {
                var key = (StateCode: license.State!.Code, license.LineOfAuthority);
                externalLookup.TryGetValue(key, out var external);

                return new ComplianceComparisonDto
                {
                    StateCode = key.StateCode,
                    LineOfAuthority = key.LineOfAuthority,
                    Local = new LocalLicenseDto
                    {
                        Id = license.Id,
                        LicenseNumber = license.LicenseNumber,
                        Status = license.Status.ToString(),
                        IssuedDate = license.IssuedDate,
                        ExpirationDate = license.ExpirationDate
                    },
                    External = external is not null
                        ? new ExternalLicenseDto
                        {
                            CeStatus = external.CeStatus,
                            CeHoursRequired = external.CeHoursCompleted,
                            CeHoursCompleted = external.CeHoursRequired,
                            NextRenewalDate = license.ExpirationDate
                        }
                        : null
                };
            }).ToArray();

            return new ComplianceReportDto
            {
                ProducerId = producer.Id,
                ProducerName = $"{producer.LastName} {producer.FirstName}",
                NpnNumber = producer.NpnNumber,
                Licenses = comparisons
            };
        }
    }
}
