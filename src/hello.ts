import {findFastestValidLap, type Lap } from "./lap";

const laps: Lap[] = [
  { lapNumber: 1, driverName: "Mira", lapTimeSeconds: 92.15, valid: true },
  { lapNumber: 2, driverName: "Mira", lapTimeSeconds: 91.42, valid: false },
  { lapNumber: 3, driverName: "Mira", lapTimeSeconds: 90.80, valid: false },
  { lapNumber: 4, driverName: "Mira", lapTimeSeconds: 91.67, valid: true },
];

const currentFastestLap = findFastestValidLap(laps);

if (currentFastestLap === undefined) {
    console.log("No valid laps were recorded.");
} else {
    console.log(`The fastest valid lap was lap number ${currentFastestLap.lapNumber} by ${currentFastestLap.driverName} with a time of ${currentFastestLap.lapTimeSeconds} seconds.`);
}



