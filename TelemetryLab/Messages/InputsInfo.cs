namespace TelemetryLab.Messages;

public class InputsInfo
{
    public InputsInfo(double throttle, double brake, double clutch, double steering)
    {
        Throttle = throttle;
        Brake = brake;
        Clutch = clutch;
        Steering = steering;
    }

    public double Throttle { get; set; }
    public double Brake { get; set; }
    public double Clutch { get; set; }
    public double Steering { get; set; }
}