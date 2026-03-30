using Infrastructure.Events.Meta;

namespace Infrastructure.Events.Fakes;

public sealed class FakeEventBus : IEventBus
{
    private readonly List<IEvent> _publishedEvents = [];

    public IReadOnlyList<IEvent> PublishedEvents => _publishedEvents.AsReadOnly();

    public Task PublishAsync<TEvent>(TEvent eventToPublish, CancellationToken cancellationToken = default)
        where TEvent : IEvent
    {
        _publishedEvents.Add(eventToPublish);
        Console.WriteLine($"[EventBus] Published event: {typeof(TEvent).Name}");
        Console.WriteLine($"[EventBus] Event data: {System.Text.Json.JsonSerializer.Serialize(eventToPublish)}");
        return Task.CompletedTask;
    }
}
