namespace TelemetryLab.Domain;

public class Car
{
    public Car(string name, string className)
    {
        Name = name;
        ClassName = className;
    }

    public string Name { get; set; }
    public string ClassName { get; set; }
}
