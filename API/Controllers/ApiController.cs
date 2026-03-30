using Infrastructure.CQRS.Meta;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    private IMediator? _mediator;

    private IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected Task<TResponse> Handle<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        return Mediator.Send(request, cancellationToken);
    }

    protected Task Handle(
        IRequest request,
        CancellationToken cancellationToken = default)
    {
        return Mediator.Send(request, cancellationToken);
    }
}
