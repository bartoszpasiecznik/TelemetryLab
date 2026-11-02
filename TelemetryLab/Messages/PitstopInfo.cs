namespace TelemetryLab.Messages;

public class PitStopInfo
{
    public PitStopInfo(double stationaryTimeS, double fuelAddedL, bool tyresChanged, bool driverSwapped, string? previousDriverName)
    {
        StationaryTimeS = stationaryTimeS;
        FuelAddedL = fuelAddedL;
        TyresChanged = tyresChanged;
        DriverSwapped = driverSwapped;
        PreviousDriverName = previousDriverName;
    }

    public double StationaryTimeS { get; set; }
    public double FuelAddedL { get; set; }
    public bool TyresChanged { get; set; }
    public bool DriverSwapped { get; set; }
    public string? PreviousDriverName { get; set; }
}