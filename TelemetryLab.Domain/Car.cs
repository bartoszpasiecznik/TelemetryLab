namespace TelemetryLab.Domain;

public class Car
{
    public Car(string carName, string className)
    {
        CarName = carName;
        ClassName = className;
    }
    
    public string CarName { get; set; }
    public string ClassName { get; set; }
}
