namespace TelemetryLab.Math;

public class Velocity
{
    public Velocity()
    {
        InMs = 0;
    }
    
    public Velocity(double ms)
    {
        InMs = ms;
    }

    public static Velocity Zero => FromMs(0);
    
    public bool IsZero => this == Zero;
    
    public double InMs { get; set; } //Default value of LMU velocity - we have to calculate all other units

    public double InKph
    {
        get => InMs * 3.6; 
        set => InMs = value / 3.6;
    }

    public double InMph
    {
        get => InMs * 2.23694; 
        set => InMs = value / 2.23694;
    }

    //calculators
    public static Velocity FromMs(double inMs)
    {
        return new Velocity(inMs);
    }

    public static Velocity FromKph(double inKph)
    {
        return new Velocity(inKph / 3.6);
    }
    
    public static Velocity FromMph(double inMph)
    {
        return new Velocity(inMph / 2.23694);
    }
    
    //operators
    public static bool operator <(Velocity a, Velocity b)
    {
        return a.InMs < b.InMs;
    }

    public static bool operator >(Velocity a, Velocity b)
    {
        return a.InMs > b.InMs;
    }

    public static bool operator <=(Velocity a, Velocity b)
    {
        return a.InMs <= b.InMs;
    }

    public static bool operator >=(Velocity a, Velocity b)
    {
        return a.InMs >= b.InMs;
    }

    public static Velocity operator -(Velocity a, Velocity b)
    {
        return FromMs(a.InMs - b.InMs);
    }
}