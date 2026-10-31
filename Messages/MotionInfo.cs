namespace TelemetryLab.Messages;

public class MotionInfo
{
    public MotionInfo(double speedKph, double rpm, int gear)
    {
        SpeedKph = speedKph;
        Rpm = rpm;
        Gear = gear;
    }

    public double SpeedKph { get; set; }
    public double Rpm { get; set; }
    public int Gear { get; set; }
}