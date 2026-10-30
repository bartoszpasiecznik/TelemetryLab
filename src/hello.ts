const driverName: string = "Pasieka";
const lapTimes: number[] = [72.5, 71.8, 73.2, 70.9];

const fastestLapTime: number = Math.min(...lapTimes);

console.log(`Driver named ${driverName} did a fastest lap time of ${fastestLapTime} seconds.`);
