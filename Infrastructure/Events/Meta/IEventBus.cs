namespace Infrastructure.Events.Meta;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent eventToPublish, CancellationToken cancellationToken = default)
        where TEvent : IEvent;
}
