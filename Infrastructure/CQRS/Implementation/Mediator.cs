using Infrastructure.CQRS.Meta;

namespace Infrastructure.CQRS.Implementation;

public sealed class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType)
            ?? throw new InvalidOperationException($"No handler registered for {request.GetType().Name}");

        var method = handlerType.GetMethod("Handle")
            ?? throw new InvalidOperationException($"Handle method not found on handler for {request.GetType().Name}");

        var result = method.Invoke(handler, [request, cancellationToken]);
        return await (Task<TResponse>)result!;
    }

    public async Task Send(IRequest request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
        var handler = _serviceProvider.GetService(handlerType)
            ?? throw new InvalidOperationException($"No handler registered for {request.GetType().Name}");

        var method = handlerType.GetMethod("Handle")
            ?? throw new InvalidOperationException($"Handle method not found on handler for {request.GetType().Name}");

        var result = method.Invoke(handler, [request, cancellationToken]);
        await (Task)result!;
    }
}
