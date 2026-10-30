namespace TelemetryLab.Models;


public class Track
{

    public Track(string name, double lapDistance)
    {
        Name = name;
        LapDistance = System.Math.Truncate(lapDistance); // Round to 2 decimal places
    }
    
    public string Name { get; set; }
    public double LapDistance { get; set; }
}