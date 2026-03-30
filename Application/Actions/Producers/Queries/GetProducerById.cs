using Common.DTOs;
using Infrastructure.CQRS.Meta;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Actions.Producers.Queries;

public static class GetProducerById
{
    public sealed record Query(Guid Id) : IRequest<ProducerDto?>;

    public sealed class Handler : IRequestHandler<Query, ProducerDto?>
    {
        private readonly AppDbContext _dbContext;

        public Handler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProducerDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var producer = await _dbContext.Producers
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (producer is null)
                return null;

            var activeLicenseCount = await _dbContext.Licenses
                .CountAsync(l => l.ProducerId == producer.Id
                    && l.Status == Domain.Models.LicenseStatus.Active, cancellationToken);

            return new ProducerDto
            {
                Id = producer.Id,
                FirstName = producer.FirstName,
                LastName = producer.LastName,
                NpnNumber = producer.NpnNumber,
                Email = producer.Email,
                ActiveLicenseCount = activeLicenseCount,
                CreatedAt = producer.CreatedAt
            };
        }
    }
}
