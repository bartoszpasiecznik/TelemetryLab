namespace TelemetryLab.Messages;

public class TickSample
{
    public TickSample(double elapsedTimeS, int lapNumber, double lapDistanceM, int position, MotionInfo motion, InputsInfo inputs, double fuelL, TyreInfo tyres)
    {
        ElapsedTimeS = elapsedTimeS;
        LapNumber = lapNumber;
        LapDistanceM = lapDistanceM;
        Position = position;
        Motion = motion;
        Inputs = inputs;
        FuelL = fuelL;
        Tyres = tyres;
    }

    public double ElapsedTimeS { get; set; }
    public int LapNumber { get; set; }
    public double LapDistanceM { get; set; }
    public int Position { get; set; }
    public MotionInfo Motion { get; set; }
    public InputsInfo Inputs { get; set; }
    public double FuelL { get; set; }
    public TyreInfo Tyres { get; set; }
}