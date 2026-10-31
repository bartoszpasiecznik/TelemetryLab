namespace TelemetryLab.Messages;

public class TyreInfo
{
    public TyreInfo(string frontCompound, string rearCompound)
    {
        FrontCompound = frontCompound;
        RearCompound = rearCompound;
    }

    public string FrontCompound { get; set;}
    public string RearCompound { get; set;}
}