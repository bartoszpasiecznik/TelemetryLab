namespace TelemetryLab.Models;

public class Driver
{
    public Driver(string driverName)
    {
        DriverName = driverName;

    }
    
    public string DriverName { get; set; }
}