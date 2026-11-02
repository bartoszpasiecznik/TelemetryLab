import {readFileSync} from "node:fs";
import {findFastestValidLap, type Lap } from "./lap";
import {type LapCompletedMessage} from "./messages";

function isRecord(value: unknown): value is Record<string, unknown> {
    return typeof value === "object" && value !== null;
}

const raw = readFileSync("src/from-csharp.json", "utf-8");
// const session = JSON.parse(raw) as {
//     lapCompleted: LapCompletedMessage[];
// };

const session: unknown = JSON.parse(raw);

if (!isRecord(session) || !Array.isArray(session.lapCompleted)) {
    throw new Error("Invalid session data");
}


const completedLaps: unknown[] = session.lapCompleted;

const laps: Lap[] = completedLaps.map((completedLap) => {
    if (!isRecord(completedLap)) {
        throw new Error("Invalid lap data");
    }
    if (
    typeof completedLap.lapNumber !== "number" ||
    typeof completedLap.driverName !== "string" ||
    typeof completedLap.lapTimeS !== "number" ||
    typeof completedLap.valid !== "boolean"
) {
    throw new Error("Invalid lap data: incorrect field types");
}
    return {
        lapNumber: completedLap.lapNumber,
        driverName: completedLap.driverName,
        lapTimeS: completedLap.lapTimeS,
        valid: completedLap.valid
    };
});

const fastestLap = findFastestValidLap(laps);

if (fastestLap) {
    console.log(`Fastest valid lap: ${fastestLap.lapNumber} by ${fastestLap.driverName} with time ${fastestLap.lapTimeS} seconds.`);
} else {
    console.log("No valid laps found.");
}



