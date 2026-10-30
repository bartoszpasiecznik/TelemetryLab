namespace TelemetryLab.Models;

public class Car
{
    public Car(string carName, string className, CarData carData)
    {
        CarName = carName;
        ClassName = className;
        CarData = carData;
    }
    
    public string CarName { get; set; }
    public string ClassName { get; set; }

    public CarData CarData { get; set; }

}