type Lap = {
  lapNumber: number;
  driverName: string;
  lapTimeS: number;
  valid: boolean;
};


export function findFastestValidLap(laps: Lap[]): Lap | undefined {
  const validLaps: Lap[] = laps.filter(lap => lap.valid);
  let currentFastestLap: Lap | undefined;

  for (const lap of validLaps) {
    if (currentFastestLap === undefined || lap.lapTimeS < currentFastestLap.lapTimeS) {
      currentFastestLap = lap;
    }
  }

  return currentFastestLap;
}
export type { Lap };