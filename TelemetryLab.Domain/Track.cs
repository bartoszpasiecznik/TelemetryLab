namespace TelemetryLab.Domain;

public class Track
{

    public Track(string name, double lapDistance)
    {
        Name = name;
        LapDistance =  lapDistance;
    }
    
    public string Name { get; set; }
    public double LapDistance { get; set; }
}
