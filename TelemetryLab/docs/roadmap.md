# TelemetryLab Roadmap

## Phase 0: Product And Technical Foundations
Goal: remove ambiguity before heavy coding.

Deliverables:
- finalized MVP scope
- chosen stack and deployment approach
- initial data model
- repository structure
- coding standards and task list

Current progress:
- initial capture/review domain entities are now defined in `TelemetryLab.Domain`
- domain files are organized into `Entities` and `Enums`

## Phase 1: Telemetry Capture Prototype
Goal: prove local collection and domain modeling.

Deliverables:
- stable telemetry reader
- session boundary detection
- lap detection and lap summary generation
- local persistence of collected sessions
- sample export for debugging

Current progress:
- the domain layer now has first-pass `Session`, `Lap`, and `TelemetrySample` models ready for collector mapping

Exit criteria:
- one user can run sessions repeatedly and inspect saved local data

## Phase 2: Backend And Storage
Goal: centralize telemetry and user data.

Deliverables:
- ASP.NET Core backend
- authentication
- database schema for users, sessions, laps, and telemetry samples
- upload API from local collector
- session processing pipeline

Exit criteria:
- collector can upload a completed session and backend stores it correctly

## Phase 3: Web App For Personal Analysis
Goal: create the first user-facing value.

Deliverables:
- login and profile
- session history
- lap detail pages
- telemetry charts
- track map playback with moving position marker
- same-user lap comparison
- cross-user lap comparison
- personal best statistics

Exit criteria:
- a user can capture a session, replay a lap visually, and compare against reference laps in the browser

## Phase 4: Shared Data And Leaderboards
Goal: enable comparison and community value.

Deliverables:
- public/private visibility controls
- track/car/class leaderboards
- cross-user benchmark summaries
- team creation and membership
- Discord notification hooks for team records

Exit criteria:
- users can compare valid laps, share data safely, and receive team record notifications

## Phase 5: Stint Planner
Goal: deliver race-planning value.

Deliverables:
- race event model
- driver assignment
- calculated stints from historical pace and fuel use
- tyre allocation planning
- planned tyre assignment by stint
- fuel-save scenario calculations
- manual event variables
- timezone-aware timeline
- calendar export

Exit criteria:
- a team can build and share a practical race plan with tyre and fuel assumptions

## Phase 6: Live Telemetry And Operational Polish
Goal: support race-day usage.

Deliverables:
- live telemetry dashboard
- low-latency updates
- planner/live view integration
- notifications and reminders
- backup, monitoring, and error diagnostics

Exit criteria:
- live view is stable enough for practice/race support
