namespace Domain.Models;

public sealed class State
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public ICollection<License>? Licenses { get; set; }
}
