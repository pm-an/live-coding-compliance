## Architecture Overview

### Backend (.NET 10 / ASP.NET Core)
- CQRS pattern with a custom Mediator implementation
- EF Core 10 in-memory database with seeded data
- Layered architecture: API → Application → Infrastructure → Domain
- External compliance service (faked) available via `IComplianceService`

### Domain
A simplified insurance producer licensing system. Producers hold licenses in different states, each with a specific line of authority (e.g. Property and Casualty, Life).

---

## Your Tasks

### Task 1: Get Producer Licenses (~15 min)

Implement the `GET /api/v1/producers/{id}/licenses` endpoint.

- Return a single producer with all their licenses, including state information (state code and name)
- Use `ProducerLicensesDto` and `LicenseDto` (already defined in `Common/DTOs/`)
- Route constant `Routes.Producers.Licenses` is already defined
- Return `404` if the producer doesn't exist

**Focus on writing an efficient EF Core query.**

### Task 2: Fix Compliance Check (~15 min)

The `GET /api/v1/producers/{id}/compliance` endpoint was implemented by another developer. It's supposed to return a **side-by-side comparison** of our local license data vs what an external compliance system reports, so the frontend can highlight discrepancies.

The endpoint compiles and is wired up, but **the frontend team reports it's crashing and returning incomplete data** for some producers.

Your job:
- Run the endpoint and investigate what's going wrong
- Find and fix the bugs in `GetProducerCompliance.cs`
- The DTOs in `Common/DTOs/ComplianceReportDto.cs` describe the expected response shape
- `IComplianceService.cs` documents the external service contract

**Hint:** There are multiple issues in the handler - some crash the endpoint, others produce wrong data.

### Bonus: Performance Review

See how we could improve `GetProducers` query handler in terms of performance.

---

## Getting Started

```bash
cd API && dotnet run
# API runs on http://localhost:5000
```

### Existing Endpoints
- `GET /api/v1/producers` - List all producers
- `GET /api/v1/producers/{id}` - Get producer by ID
- `GET /api/v1/producers/{id}/compliance` - Compliance check (buggy - Task 2)