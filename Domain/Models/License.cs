namespace Domain.Models;

public sealed class License
{
    public Guid Id { get; set; }
    public required Guid ProducerId { get; set; }
    public Producer? Producer { get; set; }
    public required Guid StateId { get; set; }
    public State? State { get; set; }
    public required string LicenseNumber { get; set; }
    public required string LineOfAuthority { get; set; }
    public required LicenseStatus Status { get; set; }
    public DateTime IssuedDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}
