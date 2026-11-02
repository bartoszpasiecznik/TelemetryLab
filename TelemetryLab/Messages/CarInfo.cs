namespace TelemetryLab.Messages;

public class CarInfo
{
    public CarInfo(string name, string className)
    {
        Name = name;
        ClassName = className;
    }

    public string Name { get; set; }
    public string ClassName { get; set; }
}