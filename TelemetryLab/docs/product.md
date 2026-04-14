# TelemetryLab Product Definition

## Vision
TelemetryLab helps Le Mans Ultimate drivers and teams capture telemetry reliably, review laps clearly, compare performance meaningfully, and build practical race plans from real session data, starting with dependable local collection and useful post-session analysis before expanding into richer team and live features.
## Core Problem
Players need one place to:

- capture and store their telemetry automatically
- review laps and compare performance over time
- compare against other drivers for the same track and car/class
- plan team races, driver rotations, fuel strategy, and tyre usage
- monitor live telemetry during practice, qualifying, and races

## Target Users
- Solo drivers who want better lap analysis and benchmarking
- Team managers planning endurance events
- Team drivers coordinating stints, swaps, and race preparation

## Product Goals
- Make telemetry capture reliable and low-friction
- Make performance analysis understandable for non-expert users
- Make endurance planning practical enough to use on race day
- Support both personal improvement and team coordination

## User Outcomes
- "I can see where I gain or lose time."
- "I can compare my laps with similar drivers."
- "I can replay a lap on a track map and inspect telemetry at any moment."
- "I can build a realistic stint plan from past pace and fuel usage."
- "My team can coordinate race strategy in one place."
- "I can monitor telemetry live without digging through raw data."

## Main Modules
- Telemetry collector running on the player's PC
- Session and lap review
- Telemetry charts and comparisons
- Leaderboards by track, car, and class
- Teams and shared planning
- Discord team notifications
- Stint planner
- Live telemetry dashboard

## High-Value Features Already Identified
- Automatic telemetry collection from Le Mans Ultimate
- Lap review with telemetry graphs and track map playback
- Historical lap and stint analysis
- Comparison with other users in v1
- Team creation and team race planning
- Driver stint planning using historical average lap time and fuel use
- Tyre allocation and usage planning within stint planning
- Variables for crashes, repairs, and driver swaps
- Timezone-aware stint viewing
- Calendar export/integration
- Fuel-save scenarios within stint planning
- Discord bot notifications for new team records by track/car/class
- Live telemetry display

## Additional Features Worth Considering
These are useful, but should not all be in v1.

- Session tags and notes
  Track conditions, setup used, event name, mistakes, learning notes
- Telemetry quality checks
  Detect missing samples, invalid laps, incomplete sessions, or corrupted uploads
- Privacy controls
  Private laps, team-only data, public leaderboard opt-in
- Incident and consistency analysis
  Off-tracks, spins, lockups, stint consistency, traffic impact
- Comparison baselines
  Personal best, team best, class best, selected reference lap
- Alerts and notifications
  Upcoming stints, swap reminders, low tyre reserve, calendar reminders
- Discord integrations
  Team channel notifications for new records, shared plans, or event reminders
- Setup and strategy snapshots
  Save the setup/strategy assumptions used for a race plan
- Manual overrides
  Adjust fuel, pace, repair time, weather assumptions, and tyre life manually
- Export tools
  CSV export, image export for charts, PDF/shareable stint plans
- Admin and moderation basics
  Report bad leaderboard data, handle duplicate tracks/cars, basic support tooling

## Product Constraints
- Single developer with limited experience
- C# preferred across the stack
- Start simple and ship a useful core before adding broad team workflows
- Avoid overengineering telemetry storage too early

## Success Criteria For First Release
- A player can install a collector, complete a session, and see uploaded laps
- A player can inspect lap time, sectors, fuel usage, telemetry graphs, and track map playback
- A player can compare telemetry against other users for the same track and car/class
- A player can build a simple stint plan from historical data
- A player can view plans in their timezone and export to calendar
- A team can share a plan and assign drivers
