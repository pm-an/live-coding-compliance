using Domain.Models;

namespace Infrastructure.Persistence;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.States.Any())
            return;

        // States
        var tx = new State { Id = Guid.Parse("a1111111-1111-1111-1111-111111111111"), Code = "TX", Name = "Texas" };
        var ca = new State { Id = Guid.Parse("a2222222-2222-2222-2222-222222222222"), Code = "CA", Name = "California" };
        var ny = new State { Id = Guid.Parse("a3333333-3333-3333-3333-333333333333"), Code = "NY", Name = "New York" };
        var fl = new State { Id = Guid.Parse("a4444444-4444-4444-4444-444444444444"), Code = "FL", Name = "Florida" };
        var il = new State { Id = Guid.Parse("a5555555-5555-5555-5555-555555555555"), Code = "IL", Name = "Illinois" };

        context.States.AddRange(tx, ca, ny, fl, il);

        // Producers
        var john = new Producer
        {
            Id = Guid.Parse("b1111111-1111-1111-1111-111111111111"),
            FirstName = "John", LastName = "Smith",
            NpnNumber = "1234567", Email = "john.smith@example.com",
            CreatedAt = DateTime.UtcNow.AddDays(-90)
        };
        var sarah = new Producer
        {
            Id = Guid.Parse("b2222222-2222-2222-2222-222222222222"),
            FirstName = "Sarah", LastName = "Johnson",
            NpnNumber = "2345678", Email = "sarah.johnson@example.com",
            CreatedAt = DateTime.UtcNow.AddDays(-60)
        };
        var michael = new Producer
        {
            Id = Guid.Parse("b3333333-3333-3333-3333-333333333333"),
            FirstName = "Michael", LastName = "Chen",
            NpnNumber = "3456789", Email = "michael.chen@example.com",
            CreatedAt = DateTime.UtcNow.AddDays(-45)
        };
        var emily = new Producer
        {
            Id = Guid.Parse("b4444444-4444-4444-4444-444444444444"),
            FirstName = "Emily", LastName = "Davis",
            NpnNumber = "4567890", Email = "emily.davis@example.com",
            CreatedAt = DateTime.UtcNow.AddDays(-30)
        };
        var robert = new Producer
        {
            Id = Guid.Parse("b5555555-5555-5555-5555-555555555555"),
            FirstName = "Robert", LastName = "Wilson",
            NpnNumber = "5678901", Email = "robert.wilson@example.com",
            CreatedAt = DateTime.UtcNow.AddDays(-10)
        };

        context.Producers.AddRange(john, sarah, michael, emily, robert);

        var now = DateTime.UtcNow;

        // Licenses - John Smith (4 licenses across 4 states)
        context.Licenses.AddRange(
            new License
            {
                Id = Guid.Parse("c1111111-1111-1111-1111-111111111111"),
                ProducerId = john.Id, StateId = tx.Id,
                LicenseNumber = "TX-PC-10001", LineOfAuthority = "Property and Casualty",
                Status = LicenseStatus.Active,
                IssuedDate = now.AddYears(-3), ExpirationDate = now.AddYears(1)
            },
            new License
            {
                Id = Guid.Parse("c2222222-2222-2222-2222-222222222222"),
                ProducerId = john.Id, StateId = ca.Id,
                LicenseNumber = "CA-LF-20001", LineOfAuthority = "Life",
                Status = LicenseStatus.Active,
                IssuedDate = now.AddYears(-2), ExpirationDate = now.AddMonths(6)
            },
            new License
            {
                Id = Guid.Parse("c3333333-3333-3333-3333-333333333333"),
                ProducerId = john.Id, StateId = ny.Id,
                LicenseNumber = "NY-PC-30001", LineOfAuthority = "Property and Casualty",
                Status = LicenseStatus.Expired,
                IssuedDate = now.AddYears(-4), ExpirationDate = now.AddMonths(-3)
            },
            new License
            {
                Id = Guid.Parse("c4444444-4444-4444-4444-444444444444"),
                ProducerId = john.Id, StateId = fl.Id,
                LicenseNumber = "FL-LF-40001", LineOfAuthority = "Life",
                Status = LicenseStatus.Active,
                IssuedDate = now.AddYears(-1), ExpirationDate = now.AddYears(2)
            },

            // Licenses - Sarah Johnson (3 licenses)
            new License
            {
                Id = Guid.Parse("c5555555-5555-5555-5555-555555555555"),
                ProducerId = sarah.Id, StateId = tx.Id,
                LicenseNumber = "TX-LF-10002", LineOfAuthority = "Life",
                Status = LicenseStatus.Active,
                IssuedDate = now.AddYears(-2), ExpirationDate = now.AddYears(1)
            },
            new License
            {
                Id = Guid.Parse("c6666666-6666-6666-6666-666666666666"),
                ProducerId = sarah.Id, StateId = ca.Id,
                LicenseNumber = "CA-PC-20002", LineOfAuthority = "Property and Casualty",
                Status = LicenseStatus.Active,
                IssuedDate = now.AddYears(-1), ExpirationDate = now.AddYears(2)
            },
            new License
            {
                Id = Guid.Parse("c7777777-7777-7777-7777-777777777777"),
                ProducerId = sarah.Id, StateId = il.Id,
                LicenseNumber = "IL-LF-50001", LineOfAuthority = "Life",
                Status = LicenseStatus.Pending,
                IssuedDate = now.AddDays(-30), ExpirationDate = now.AddYears(3)
            },

            // Licenses - Michael Chen (2 licenses)
            new License
            {
                Id = Guid.Parse("c8888888-8888-8888-8888-888888888888"),
                ProducerId = michael.Id, StateId = ny.Id,
                LicenseNumber = "NY-PC-30002", LineOfAuthority = "Property and Casualty",
                Status = LicenseStatus.Active,
                IssuedDate = now.AddYears(-2), ExpirationDate = now.AddYears(1)
            },
            new License
            {
                Id = Guid.Parse("c9999999-9999-9999-9999-999999999999"),
                ProducerId = michael.Id, StateId = fl.Id,
                LicenseNumber = "FL-PC-40002", LineOfAuthority = "Property and Casualty",
                Status = LicenseStatus.Active,
                IssuedDate = now.AddYears(-1), ExpirationDate = now.AddYears(2)
            },

            // Licenses - Emily Davis (3 licenses)
            new License
            {
                Id = Guid.Parse("ca111111-1111-1111-1111-111111111111"),
                ProducerId = emily.Id, StateId = tx.Id,
                LicenseNumber = "TX-PC-10003", LineOfAuthority = "Property and Casualty",
                Status = LicenseStatus.Active,
                IssuedDate = now.AddYears(-1), ExpirationDate = now.AddYears(2)
            },
            new License
            {
                Id = Guid.Parse("cb111111-1111-1111-1111-111111111111"),
                ProducerId = emily.Id, StateId = ca.Id,
                LicenseNumber = "CA-LF-20003", LineOfAuthority = "Life",
                Status = LicenseStatus.Expired,
                IssuedDate = now.AddYears(-3), ExpirationDate = now.AddMonths(-6)
            },
            new License
            {
                Id = Guid.Parse("cc111111-1111-1111-1111-111111111111"),
                ProducerId = emily.Id, StateId = ny.Id,
                LicenseNumber = "NY-LF-30003", LineOfAuthority = "Life",
                Status = LicenseStatus.Active,
                IssuedDate = now.AddMonths(-8), ExpirationDate = now.AddYears(2)
            },

            // Licenses - Robert Wilson (1 license)
            new License
            {
                Id = Guid.Parse("cd111111-1111-1111-1111-111111111111"),
                ProducerId = robert.Id, StateId = tx.Id,
                LicenseNumber = "TX-PC-10004", LineOfAuthority = "Property and Casualty",
                Status = LicenseStatus.Pending,
                IssuedDate = now.AddDays(-5), ExpirationDate = now.AddYears(3)
            }
        );

        context.SaveChanges();
    }
}
