using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Producer> Producers => Set<Producer>();
    public DbSet<License> Licenses => Set<License>();
    public DbSet<State> States => Set<State>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(2);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.NpnNumber).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LicenseNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LineOfAuthority).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);
            entity.HasOne(e => e.Producer)
                  .WithMany(p => p.Licenses)
                  .HasForeignKey(e => e.ProducerId);
            entity.HasOne(e => e.State)
                  .WithMany(s => s.Licenses)
                  .HasForeignKey(e => e.StateId);
        });
    }
}
