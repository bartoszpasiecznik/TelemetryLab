namespace TelemetryLab.Messages;

public class LapCompletedMessage
{
    public LapCompletedMessage(string type, string sessionId, int lapNumber, string driverName, double lapTimeS, double bestLapTimeS, double fuelUsedL, PitStopInfo? pitStop)
    {
        Type = type;
        SessionId = sessionId;
        LapNumber = lapNumber;
        DriverName = driverName;
        LapTimeS = lapTimeS;
        BestLapTimeS = bestLapTimeS;
        FuelUsedL = fuelUsedL;
        PitStop = pitStop;
    }

    public string Type { get; set; }
    public string SessionId { get; set; }
    public int LapNumber { get; set; }
    public string DriverName { get; set; }
    public double LapTimeS { get; set; }
    public double BestLapTimeS { get; set; }
    public double FuelUsedL { get; set; }
    public PitStopInfo? PitStop { get; set; }
}