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

## Current task
Session data is connected to the live telemetry loop and prints successfully.

## Next steps
1. After those values are verified, plan how to package one complete telemetry snapshot for the TypeScript backend

Defer sector times for now; add them later.

## Progress log
- Completed: TypeScript lap-time array exercise.
- Completed: Session model is connected to live data and prints in `TelemetryLoop`.
- Completed: `Driver` model uses the player's name from scoring data and prints it in `TelemetryLoop`.
-  Connect the existing `Car` and `Track` models to their live source fields.
- Verify the `Lap` model output against the game.
- Add remaining telemetry fields one at a time: speed/gear, throttle/brake, track position, fuel, and tyre temperature/wear.
