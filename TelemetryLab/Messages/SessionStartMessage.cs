namespace TelemetryLab.Messages;

public class SessionStartMessage
{
    public SessionStartMessage(string type, string sessionId, DateTime startedAt, int sessionType, TrackInfo track, CarInfo car)
    {
        this.Type = type;
        SessionId = sessionId;
        StartedAt = startedAt;
        SessionType = sessionType;
        Track = track;
        Car = car;

    }

    public string Type { get; set; }
    public string SessionId { get; set; }
    public DateTime StartedAt { get; set; }
    public int SessionType { get; set; }
    public TrackInfo Track { get; set; }
    public CarInfo Car { get; set; }

}