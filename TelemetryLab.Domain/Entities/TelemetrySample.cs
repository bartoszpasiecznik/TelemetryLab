namespace TelemetryLab.Domain;

public class TelemetrySample
{
    public TelemetrySample(
        Lap lap,
        TimeSpan relativeTimeOffset,
        double lapDistance,
        double speed,
        double throttle,
        double brake,
        double clutch,
        double steering,
        int gear,
        int engineRPM,
        double positionX,
        double positionY,
        double positionZ)
    {
        Lap = lap;
        RelativeTimeOffset = relativeTimeOffset;
        LapDistance = lapDistance;
        Speed = speed;
        Throttle = throttle;
        Brake = brake;
        Clutch = clutch;
        Steering = steering;
        Gear = gear;
        EngineRPM = engineRPM;
        PositionX = positionX;
        PositionY = positionY;
        PositionZ = positionZ;
    }

    public Guid Id { get; set; }
    public Lap Lap { get; set; }
    public TimeSpan RelativeTimeOffset { get; set; }
    public double LapDistance { get; set; }
    public double Speed { get; set; }
    public double Throttle { get; set; }
    public double Brake { get; set; }
    public double Clutch { get; set; }
    public double Steering { get; set; }
    public int Gear { get; set; }
    public int EngineRPM { get; set; }
    public double PositionX { get; set; }
    public double PositionY { get; set; }
    public double PositionZ { get; set; }
}
