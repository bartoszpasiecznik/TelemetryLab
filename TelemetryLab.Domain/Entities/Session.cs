namespace TelemetryLab.Domain;

public class Session
{
    public Session(
        Track track,
        Car car,
        DateTime startedAtUtc,
        DateTime? endedAtUtc,
        SessionType sessionType)
    {
        Track = track;
        Car = car;
        StartedAtUtc = startedAtUtc;
        EndedAtUtc = endedAtUtc;
        SessionType = sessionType;
    }
    
    public Guid Id { get; set; }
    public Track Track { get; set; }
    public Car Car { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public DateTime? EndedAtUtc { get; set; }
    public SessionType SessionType { get; set; }
}