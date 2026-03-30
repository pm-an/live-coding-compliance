using Common.DTOs;
using Infrastructure.CQRS.Meta;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Actions.Producers.Queries;

public static class GetProducers
{
    public sealed record Query(string? Search = null) : IRequest<ProducerDto[]>;

    public sealed class Handler : IRequestHandler<Query, ProducerDto[]>
    {
        private readonly AppDbContext _dbContext;

        public Handler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProducerDto[]> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Producers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();
                query = query.Where(p =>
                    p.FirstName.ToLower().Contains(search) ||
                    p.LastName.ToLower().Contains(search) ||
                    p.NpnNumber.Contains(search));
            }

            var producers = await query
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync(cancellationToken);

            var result = new List<ProducerDto>();

            foreach (var producer in producers)
            {
                var activeLicenseCount = await _dbContext.Licenses
                    .CountAsync(l => l.ProducerId == producer.Id
                        && l.Status == Domain.Models.LicenseStatus.Active, cancellationToken);

                result.Add(new ProducerDto
                {
                    Id = producer.Id,
                    FirstName = producer.FirstName,
                    LastName = producer.LastName,
                    NpnNumber = producer.NpnNumber,
                    Email = producer.Email,
                    ActiveLicenseCount = activeLicenseCount,
                    CreatedAt = producer.CreatedAt
                });
            }

            return result.ToArray();
        }
    }
}
