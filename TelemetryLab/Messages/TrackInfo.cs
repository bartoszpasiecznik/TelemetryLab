namespace TelemetryLab.Messages;

public class TrackInfo
{
    public TrackInfo(string name, double lapLengthM)
    {
        Name = name;
        LapLengthM = lapLengthM;
    }

    public string Name { get; set; }
    public double LapLengthM { get; set; }
}