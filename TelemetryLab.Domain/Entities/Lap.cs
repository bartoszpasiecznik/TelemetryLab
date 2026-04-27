namespace TelemetryLab.Domain;

public class Lap
{
    public Lap(
        Session session,
        int lapNumber,
        bool isValid,
        TimeSpan lapTime,
        TimeSpan? sector1Time,
        TimeSpan? sector2Time,
        TimeSpan? sector3Time,
        double fuelUsed,
        double? virtualEnergyUsed,
        double topSpeed,
        TyreType tyreType)
    {
        Session = session;
        LapNumber = lapNumber;
        IsValid = isValid;
        LapTime = lapTime;
        Sector1Time = sector1Time;
        Sector2Time = sector2Time;
        Sector3Time = sector3Time;
        FuelUsed = fuelUsed;
        VirtualEnergyUsed = virtualEnergyUsed;
        TopSpeed = topSpeed;
        TyreType = tyreType;
    }
    
    public Guid Id { get; set; }
    public Session Session { get; set; }
    public int LapNumber { get; set; }
    public bool IsValid { get; set; }
    public TimeSpan LapTime { get; set; }
    public TimeSpan? Sector1Time { get; set; }
    public TimeSpan? Sector2Time { get; set; }
    public TimeSpan? Sector3Time { get; set; }
    public double FuelUsed { get; set; }
    public double? VirtualEnergyUsed { get; set; }
    public double TopSpeed { get; set; }
    public TyreType TyreType { get; set; }
}