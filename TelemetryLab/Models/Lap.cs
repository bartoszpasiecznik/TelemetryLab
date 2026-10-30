namespace TelemetryLab.Models;

public class Lap
{

    public Lap( int lapNumber, double lastLapTime, double bestLapTime)
    {
        LapNumber = lapNumber;
        LastLapTime = lastLapTime;
        BestLapTime = bestLapTime;
    }
    public int LapNumber { get; set; }
    public double LastLapTime { get; set; }
    public double BestLapTime { get; set; }
}