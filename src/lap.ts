type Lap = {
  lapNumber: number;
  driverName: string;
  lapTimeSeconds: number;
  valid: boolean;
};


export function findFastestValidLap(laps: Lap[]): Lap | undefined {
  const validLaps: Lap[] = laps.filter(lap => lap.valid);
  let currentFastestLap: Lap | undefined;

  for (const lap of validLaps) {
    if (currentFastestLap === undefined || lap.lapTimeSeconds < currentFastestLap.lapTimeSeconds) {
      currentFastestLap = lap;
    }
  }

  return currentFastestLap;
}
export type { Lap };