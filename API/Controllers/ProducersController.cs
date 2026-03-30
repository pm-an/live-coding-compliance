using Application.Actions.Producers.Queries;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route(Routes.Producers.Prefix)]
public class ProducersController : ApiController
{
    [HttpGet]
    public async Task<ProducerDto[]> GetProducers(
        [FromQuery] string? search,
        CancellationToken cancellationToken)
    {
        return await Handle(new GetProducers.Query(search), cancellationToken);
    }

    [HttpGet(Routes.Producers.ById)]
    public async Task<ActionResult<ProducerDto>> GetProducerById(Guid id, CancellationToken cancellationToken)
    {
        var producer = await Handle(new GetProducerById.Query(id), cancellationToken);

        if (producer is null)
            return NotFound();

        return producer;
    }

    // TODO: Task 1 - GET /api/v1/producers/{id}/licenses

    [HttpGet(Routes.Producers.Compliance)]
    public async Task<ActionResult<ComplianceReportDto>> GetProducerCompliance(Guid id, CancellationToken cancellationToken)
    {
        var result = await Handle(new GetProducerCompliance.Query(id), cancellationToken);

        if (result is null)
            return NoContent();

        return result;
    }
}
