# AGENTS.md

## Project Context
TelemetryLab is a simracing telemetry and race-planning platform for Le Mans Ultimate.

The product goal is to:

- collect telemetry from the local player's machine
- store and analyze laps and sessions
- let users compare telemetry and leaderboard results
- support team race planning for endurance events
- provide strategy tools such as stint planning, tyre planning, and fuel-save calculations
- post team record notifications to Discord
- support live telemetry viewing later


## Product Priorities
Build in this order:

1. Reliable telemetry capture
2. Session and lap review
3. Historical analysis
4. Team sharing and leaderboards
5. Stint planner
6. Tyre planner and fuel-save tools
7. Live telemetry

The first release should prove:

- telemetry ingestion is stable
- lap review is useful
- stint planning works from real historical data

## MVP Scope
In scope for the first release:

- local telemetry collection for Le Mans Ultimate
- local persistence of collected session data
- upload of completed sessions
- user accounts and timezone support
- session and lap review
- telemetry graphs with track map playback
- same-user and cross-user lap comparison
- track/car and track/class leaderboards
- teams and shared plans
- Discord team record notifications
- simple stint planner
- timezone-aware stint display
- calendar export
- tyre planner v1
- fuel-save calculator v1

Out of scope for the first release unless explicitly requested:

- mobile apps
- payments/subscriptions
- advanced real-time race control tools
- full weather simulation
- setup file parsing
- overly complex admin tooling
- microservices

## Technical Direction
Prefer a simple C# modular monolith.

Recommended solution layout:

- `TelemetryLab.Collector`
- `TelemetryLab.Api`
- `TelemetryLab.Web`
- `TelemetryLab.Domain`
- `TelemetryLab.Infrastructure`

Recommended stack:

- C#
- ASP.NET Core
- Blazor
- EF Core
- PostgreSQL
- SignalR later for live features

General rules:

- keep business logic out of controllers and UI components
- isolate raw telemetry parsing from analysis and planning logic
- prefer explicit domain services for calculations
- store enough metadata to recompute summaries later
- avoid premature optimization and avoid microservice decomposition

## Collaboration Rules
The user wants guidance, planning, and implementation help, not blind code substitution.

When helping on this repo:

- prefer guiding the user through small, concrete steps
- do not complete coding tasks unless the user explicitly asks for implementation
- default to explaining how to approach the task, what to do first, and what to watch for
- suggest practical implementation options when the user is unsure
- break work into tasks that fit into short sessions
- explain tradeoffs when recommending architecture or libraries
- challenge scope creep early
- prioritize simple and debuggable solutions
- keep docs aligned with actual implementation decisions

## Source Of Truth Docs
Use these files when planning or implementing:

- `docs/product.md`
- `docs/mvp.md`
- `docs/roadmap.md`
- `docs/architecture.md`
- `docs/tasks.md`

If implementation decisions change, update the relevant doc.

## Features That Are Easy To Forget
Consider these during planning and reviews:

- privacy controls for shared telemetry
- telemetry quality validation
- invalid/incomplete session handling
- consistency and incident analysis
- manual overrides in planning calculations
- timezone correctness
- duplicate notification prevention
- graph and track map synchronization
- export/import needs
- reminders and notifications
- logging and diagnostics

## Development Strategy
Default workflow:

1. clarify the requirement
2. check relevant docs
3. reduce the change to a small task
4. implement the smallest useful slice
5. verify behavior
6. update docs if assumptions changed

Build vertical slices where possible:

- capture session
- persist session
- upload session
- review session

Do not jump to advanced team planning or live telemetry until historical session handling is trustworthy.

## Current Repo State
The repo already contains early telemetry reader code under the existing `TelemetryLab` project. Treat that code as the starting prototype for the future collector application.
