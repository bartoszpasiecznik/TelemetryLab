# Simracing Tool: Project Notes

## Goal
- Help manage simracing teams for endurance races
- Let drivers compare telemetry of their laps against other users' laps to improve
- Game: Le Mans Ultimate (existing C# shared memory reader)
- Stack: TypeScript (logic + frontend), PostgreSQL

## How I'm learning
I'm a beginner. Claude acts as a guide: explain concepts, give hints, let me try first, then review my code. No full ready-made solutions.

## Architecture
```
Le Mans Ultimate -> C# collector -> TypeScript backend -> PostgreSQL
                                          |
                                    TypeScript frontend (charts, comparison, team tools)
```
TypeScript can't easily read shared memory, so the C# reader stays as a small collector.

## Tools (add each when its phase starts)
- VS Code, Node.js (LTS), Git + GitHub
- PostgreSQL + DBeaver
- Fastify (backend), Prisma (database layer)
- React + Vite (frontend), uPlot or ECharts (charts)

## Phases
- [ ] Phase 0: Setup (VS Code, Node, Git, hello world)
- [ ] Phase 1: Learn just enough TypeScript using lap-time exercises
- [ ] Phase 2: Design the data on paper (session, driver, stint, lap, telemetry sample)
- [ ] Phase 3: Read an exported session file in TS and print the fastest lap
- [ ] Phase 4: Load data into PostgreSQL (store a lap's samples as one JSONB block at first)
- [ ] Phase 5: Backend API (GET /laps, GET /laps/:id)
- [ ] Phase 6: Frontend: lap table + speed-over-distance chart
- [ ] Phase 7: Compare two laps (aligned by track distance, show time delta)
- [ ] Phase 8: Live data from the collector to the backend
- [ ] Phase 9: Team/endurance features (stints, fuel, tyres, driver swaps, user accounts)

## Phase 0 setup steps
1. Install VS Code, Node.js (LTS), Git
2. Check in the terminal (Ctrl + `): `node --version`, `npm --version`, `git --version`
3. Create folder `simracing-tool`, open it with File > Open Folder
4. `npm init -y` (creates package.json)
5. `npm install --save-dev typescript tsx`, then `npx tsc --init`
6. Create `src/hello.ts`, run with `npx tsx src/hello.ts`
7. `git init`, add `.gitignore` containing `node_modules`, then `git add .` and `git commit -m "Initial setup"`
8. Optional extensions: Prettier, ESLint

## TypeScript implementation steps
Learn each concept with mock data first; keep the code small and ask for hints/review rather than a full solution.

- [x] Finish the mock lap exercise in `src/hello.ts`: define a `Lap` type, annotate the array as `Lap[]`, filter valid laps, find the fastest valid lap, and print its driver, lap number, and time.
- [x] Check the exercise with edge cases: no laps, no valid laps, and a tie. Make the fastest-lap logic return `undefined` when there is no valid result.
- [x] Move the lap type and fastest-lap logic into a small module under `src/` once the exercise works; keep `hello.ts` as a simple runner.
- [x] Use the message shapes in `docs/snapshot.json` to define TypeScript types for `sessionStart`, `lapCompleted`, and `tickBatch`.
- [x] Create a small mock session JSON file matching that snapshot and load it from TypeScript. Print the fastest valid completed lap; keep the sample data small enough to inspect by hand.
- [x] Try missing or incorrectly typed JSON fields and decide how to report invalid input instead of assuming parsed JSON matches a TypeScript type.
- [ ] Serialize the C# message classes to camelCase JSON, then load that output in TypeScript and confirm it produces the same result as the mock fixture.
- [ ] Only after the file-based flow works, continue with PostgreSQL and the API phases below.

## Current task
In progress: build the C# message classes that match `docs/snapshot.json`, in `TelemetryLab/Messages/` (namespace `TelemetryLab.Messages`).

Pattern: one class per JSON `{ }` block, one PascalCase property per field, constructor sets every property. Build from the inside out.

- [x] `MotionInfo`
- [x] `InputsInfo`
- [x] `TyreInfo`
- [x] `TickSample` (uses `MotionInfo`, `InputsInfo`, `TyreInfo`)
- [x] `TickBatchMessage` (list of `TickSample`)
- [x] `TrackInfo`, `CarInfo`
- [x] `SessionStartMessage`
- [x] `PitStopInfo`
- [x] `LapCompletedMessage` (`PitStop` can be null)
- [ ] Next: serialize to camelCase JSON with `System.Text.Json` (`JsonNamingPolicy.CamelCase`)

## Snapshot design decisions (see `docs/snapshot.json`)
- Three message types: `sessionStart` (once), `lapCompleted` (per lap), `tickBatch` (batched 30 Hz samples)
- Each `lapCompleted` message includes a `valid` boolean for lap validity
- Driver and pit stop info live on `lapCompleted` (driver swaps happen mid-session); `pitStop` is `null` on laps without a stop
- Ticks are sent in batches (~1 per second) with a `batchSeq` counter so the backend can spot gaps and duplicates
- Units are in field names (`Kph`, `M`, `S`, `L`); the collector does all conversions

## Next steps
1. Add track position (`VehicleScoringInfoV01.mLapDist`)
2. Fix small issues in `TelemetryLoop`: `curSpeed` is computed after `CarData` is built (one tick late); debug print labels are wrong
3. Fix the misleading "Round to 2 decimal places" comment in `Track` (code uses `Truncate`)

Defer sector times and per-wheel tyre data (temperature/wear/pressure from `TelemInfoV01.mWheel[4]`) for now; add them later.

## Progress log
- Completed: TypeScript lap-time array exercise.
- Completed: Session model is connected to live data and prints in `TelemetryLoop`.
- Completed: `Driver` model uses the player's name from scoring data and prints it in `TelemetryLoop`.
- Completed: `Car` (name, class) and `CarData` (position, RPM, speed, gear, throttle, brake, clutch, steering, fuel, tyre compounds) connected to live data.
- Completed: `Track` (name, lap length) connected to live data.
- Completed: `Lap` (lap number, last/best lap time).
- Completed: Snapshot message design (`docs/snapshot.json`).

## Needs in-game verification (requires Windows + LMU)
- `Car` / `CarData`: all fields, especially speed (km/h) and steering/throttle ranges
- `Track`: name and lap length
- Track position, once added
- Sign of `mFilteredSteering` (which way is left?)
- Which driver the game reports on the lap where a driver swap happens
