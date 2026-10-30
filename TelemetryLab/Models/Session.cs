namespace TelemetryLab.Models;

public class Session
{
    public Session(int sessionType)
    {
        SessionType = sessionType;
    }
    
    public int SessionType { get; set; }
}