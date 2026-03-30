namespace Domain.Models;

public sealed class Producer
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string NpnNumber { get; set; }
    public required string Email { get; set; }
    public ICollection<License>? Licenses { get; set; }
    public DateTime CreatedAt { get; set; }
}
