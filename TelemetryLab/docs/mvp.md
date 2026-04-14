# TelemetryLab MVP Scope

## MVP Goal
Ship a first version that proves three things:

- telemetry collection works reliably
- lap review is genuinely useful
- simple team race planning can be done with real historical data

## In Scope For MVP

### 1. Local telemetry collection
- Read telemetry from Le Mans Ultimate on the player's PC
- Detect session start/end
- Store raw session data locally before upload
- Upload completed session summaries and selected telemetry samples to backend

### 2. Accounts and identity
- Basic user accounts
- Login/logout
- User profile with timezone

### 3. Session and lap review
- Session list
- Lap list within a session
- Basic lap details:
  lap time, sector times, fuel used, invalid/valid lap, top speed
- Telemetry charts for one lap:
  speed, throttle, brake, gear, RPM
- Track map for one lap with a movable playback marker
- Synchronize graph cursor and track position for a selected moment in the lap
- Compare two laps from the same user

### 4. Historical analysis
- Best lap per track/car
- Average lap time and fuel consumption per track/car
- Stint summary statistics:
  average pace, total laps, total fuel, consistency

### 5. Simple leaderboards
- Leaderboards by track + car
- Leaderboards by track + class
- Filters for valid laps only
- Select another user as a lap comparison reference from the leaderboard
- Compare telemetry traces and track position against another user's lap

### 6. Teams
- Create team
- Invite members
- Share sessions and plans with team members
- Configure a Discord channel webhook or bot connection for a team
- Post a Discord message when a team member sets a new fastest valid lap for a selected:
  track + car, track + class

### 7. Stint planner v1
- Create race plan with race length
- Assign drivers
- Calculate suggested stints from historical average lap time and fuel use
- Define tyre allocation for an event
- Estimate tyre usage from historical stint length and pace
- Assign planned tyre sets to stints manually
- Show estimated laps per tank
- Show target lap time delta needed to save a given fuel amount
- Allow manual fuel-save scenario inputs
- Manual adjustments for:
  extra pit time, repairs, driver swap delay
- Show all stint times in selected timezone
- Export plan to calendar file

## Explicitly Out Of Scope For MVP
- Live telemetry streaming to other users
- Real-time race control style dashboards for teams
- Full weather simulation
- Setup file parsing and setup analytics
- Mobile app
- Advanced overlays and coaching tools
- Payments/subscriptions
- Complex admin tooling

## MVP Non-Functional Requirements
- A failed upload must not lose a player's collected session
- Uploaded telemetry must be tied to track, car, and session metadata
- Planner calculations must be explainable, not "black box"
- Timezone handling must be correct and explicit
- UI should work on desktop first; mobile can be basic
- Team record notifications must avoid duplicate or invalid-lap announcements
- Telemetry playback must keep chart position and track map position synchronized

## What Makes The MVP Good Enough To Release
- Telemetry ingestion is stable for repeated sessions
- Session review is fast and understandable
- Cross-user comparison is useful enough to highlight gains and losses
- Stint planner results are close enough to be useful in a real race-prep workflow
- Team members can share one plan without confusion

## MVP Confirmation
Confirmed with one scope adjustment: tyre allocation planning and fuel-save calculations are part of `Stint planner v1`, not separate MVP modules.
