# TelemetryLab Architecture Outline

## Recommendation
Use a modular monolith in C# first. Do not split into microservices.

Recommended high-level parts:

- `TelemetryLab.Collector`
  Desktop or console app running on the player's machine, responsible for reading telemetry and uploading it
- `TelemetryLab.Api`
  ASP.NET Core backend for authentication, uploads, planning logic, and APIs
- `TelemetryLab.Web`
  Blazor web frontend for dashboards, lap review, leaderboards, teams, and planning
- `TelemetryLab.Domain`
  Shared core models and business rules
- `TelemetryLab.Infrastructure`
  Database, file storage, external integrations

## Why This Stack
You want C# and you are a solo developer with limited experience. The simplest maintainable route is:

- C# end to end
- ASP.NET Core for backend
- Blazor for frontend
- EF Core for data access
- PostgreSQL for relational storage
- SignalR for live telemetry updates later

This minimizes language switching while still giving you a path to scale.

## Suggested Technology Choices

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- FluentValidation
- Serilog for logging

### Frontend
- Blazor Web App
- A charting library later for telemetry visualization
- Start with server-side rendered or interactive Blazor pages, not a complex SPA architecture

### Database
- PostgreSQL
- Store summarized telemetry in relational tables first
- Keep raw telemetry payloads in compressed files or blobs if needed

### Auth
- Start simple with ASP.NET Core Identity
- Add external login providers later if useful

### Background Processing
- Start with hosted background services
- Add Hangfire or Quartz only if scheduling and retry logic become painful

### Deployment
- Start with one backend app + one database
- Deploy to a simple VPS or cloud app service
- Add object storage later if raw telemetry files grow

## Important Domain Boundaries

### Telemetry ingestion
Responsibilities:
- connect to local LMU telemetry source
- map raw data into session/lap/sample models
- handle disconnects and invalid reads
- persist locally before upload

### Session analysis
Responsibilities:
- detect laps and sectors
- summarize pace, fuel, consistency, and tyre-related metrics
- validate whether a lap should count for leaderboards
- persist enough position data for track map playback and lap comparison

### Planning engine
Responsibilities:
- compute stint durations
- estimate fuel windows
- model tyre allocation and planned tyre usage
- apply manual adjustments
- support fuel-save scenarios

### Collaboration
Responsibilities:
- teams
- shared plans
- permissions
- visibility controls
- outbound notifications for team events and records

### Visualization and comparison
Responsibilities:
- convert telemetry samples into chart-friendly series
- reconstruct track path from position samples
- synchronize selected timestamp, graph cursor, and track marker
- compare a player's lap against another user's reference lap

### Integrations
Responsibilities:
- Discord bot or webhook notifications
- calendar export
- future external notifications

## Data Model Starting Point

Core entities:
- User
- Team
- TeamMember
- TeamNotificationChannel
- Car
- Track
- Session
- Lap
- TelemetrySample
- SessionUpload
- RacePlan
- RacePlanDriver
- PlannedStint
- TyreAllocation
- TyreSet

Current implemented first pass in `TelemetryLab.Domain`:
- `Entities/Session`
- `Entities/Lap`
- `Entities/TelemetrySample`
- `Entities/Track`
- `Entities/Car`
- `Enums/SessionType`
- `Enums/TyreType`

Current modeling decisions:
- `Session` owns `Track` and `Car`
- `Lap` belongs to `Session` and stores lap summaries such as lap time, sector times, fuel used, virtual energy used, top speed, validity, and tyre type
- `TelemetrySample` belongs to `Lap` and stores lap-relative telemetry points for charts and track playback
- `TelemetrySample.RelativeTimeOffset` is lap-relative
- speed values are intended to be stored in `km/h`
- `Track` currently keeps `LapDistance` as track-layout metadata unless future telemetry mapping proves it unreliable

## Architecture Rules
- Keep business logic out of controllers and UI components
- Prefer explicit domain services for calculations
- Keep raw telemetry parsing isolated from planning logic
- Version telemetry import formats so future game changes are survivable
- Store enough source metadata to recompute summaries later
- Keep outbound notification logic behind an interface so Discord is replaceable
- Preserve timestamped position samples needed for lap playback

## Risks To Watch Early
- Telemetry volume becoming too large too quickly
- Incorrect session/lap detection
- Track map playback becoming inaccurate because of missing or sparse position data
- Timezone bugs in planning
- Leaderboards polluted by bad or incomparable data
- Duplicate or noisy Discord notifications reducing trust
- Trying to build live telemetry before historical analysis is stable

## First Technical Milestones
1. Make the collector save valid local sessions reliably.
2. Define the database schema for sessions, laps, and summaries.
3. Build upload + processing for completed sessions.
4. Build session review with graphs and track map playback.
5. Add cross-user comparison once lap normalization is trustworthy.
6. Add team leaderboard notifications once valid-lap processing is trustworthy.
7. Add planning only after historical data is trustworthy.

## Stack Confirmation
Confirmed as-is: use a C# modular monolith with ASP.NET Core, Blazor, EF Core, PostgreSQL, hosted background services first, and SignalR later for live features.

## Collector Direction
For v1, the collector is console-first. A desktop UI can be added later once telemetry capture, local persistence, and uploads are stable.

## Upload Direction
For v1, telemetry uploads happen only after session end. Live or in-session uploads are deferred until historical session capture and processing are stable.
