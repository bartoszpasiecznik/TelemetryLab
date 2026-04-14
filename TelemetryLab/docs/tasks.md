# TelemetryLab Task Breakdown

## Current Priority Order

### Track 1: Discovery And Decisions
- [x] Write a one-paragraph user promise for the product
- [x] Confirm MVP scope from `docs/mvp.md`
- [x] Confirm recommended stack from `docs/architecture.md`
- [x] Decide whether the collector is console-first or desktop UI-first
- [x] Decide whether telemetry uploads happen after session end only for v1

### Track 2: Repository Setup
- [ ] Split the solution into projects:
  collector, api, web, domain, infrastructure
- [ ] Add shared coding conventions
- [ ] Add configuration handling for local development
- [ ] Add logging and error handling baseline

### Track 3: Collector Prototype
- [ ] Verify current telemetry reader works repeatedly
- [ ] Define raw telemetry DTOs
- [ ] Detect session start and end
- [ ] Detect lap completion
- [ ] Save local session file
- [ ] Save lap summaries
- [ ] Save timestamped position samples needed for track map playback
- [ ] Add simple debug output for validation

### Track 4: Domain Modeling
- [ ] Define entities for users, sessions, laps, and telemetry samples
- [ ] Define entities for race plans, drivers, and stints
- [ ] Define entities for tyre allocations
- [ ] Define rules for valid laps and leaderboard eligibility
- [ ] Define how lap samples are aligned for same-user and cross-user comparisons

### Track 5: Backend
- [ ] Create ASP.NET Core API project
- [ ] Add database and EF Core migrations
- [ ] Create authentication flow
- [ ] Create upload endpoint for completed sessions
- [ ] Create processing service for uploaded sessions
- [ ] Create session list and session detail endpoints
- [ ] Add team fastest-lap evaluation rules for track/car and track/class
- [ ] Add notification event generation for new team records

### Track 6: Web App
- [ ] Create Blazor web app project
- [ ] Add login and profile pages
- [ ] Build session list page
- [ ] Build lap detail page
- [ ] Add telemetry chart page with synchronized selection marker
- [ ] Add track map playback with moving dot for the selected timestamp
- [ ] Add same-user lap comparison page
- [ ] Add cross-user lap comparison page from leaderboard-selected reference laps

### Track 7: Teams And Planning
- [ ] Add team creation and membership
- [ ] Add team notification settings for Discord
- [ ] Create Discord message format for new team records
- [ ] Send a Discord notification when a valid lap becomes a new team best
- [ ] Create race plan entity and UI
- [ ] Build stint planner calculation service
- [ ] Add tyre allocation support to stint planner
- [ ] Add fuel-save scenarios to stint planner
- [ ] Add timezone conversion support
- [ ] Add calendar export

### Track 8: Notification Quality
- [ ] Prevent duplicate record notifications
- [ ] Ignore invalid laps and incomplete uploads
- [ ] Add audit logging for sent notifications
- [ ] Add test cases for record detection rules

## Suggested 1-3 Hour Starter Tasks
- [ ] Document what telemetry fields are currently available from the reader
- [ ] Verify the telemetry feed includes position data suitable for drawing a track map
- [ ] Create a sample local session JSON file from one run
- [ ] Write the `Session`, `Lap`, and `TelemetrySample` domain classes
- [ ] Create a simple ERD or table list for the core database
- [ ] Scaffold the ASP.NET Core API project
- [ ] Scaffold the Blazor web app project

## Definition Of Done For A Task
- Code is understandable and named clearly
- Failure cases are handled or explicitly deferred
- Logging exists where debugging would otherwise be difficult
- The change is manually verified
- The related docs are updated if assumptions changed
